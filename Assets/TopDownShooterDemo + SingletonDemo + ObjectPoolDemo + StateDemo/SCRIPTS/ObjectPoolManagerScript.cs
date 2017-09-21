﻿/*
 * @Author: David Crook
 *
 * Use this singleton Object Pooling Manager Class to manage a series of object pools.
 * Typical uses are for particle effects, bullets, enemies etc.
 *
 *
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectPoolManagerScript
{

	//the variable is declared to be volatile to ensure that
	//assignment to the instance variable completes before the
	//instance variable can be accessed.
	private static volatile ObjectPoolManagerScript instance;

	//look up list of various object pools.
	private Dictionary<String, ObjectPoolScript> objectPools;

	//object for locking
	private static object syncRoot = new System.Object();

	/// <summary>
	/// Constructor for the class.
	/// </summary>
	private ObjectPoolManagerScript()
	{
		//Ensure object pools exists.
		this.objectPools = new Dictionary<String, ObjectPoolScript>();
	}

	/// <summary>
	/// Property for retreiving the singleton.  See msdn documentation.
	/// </summary>
	public static ObjectPoolManagerScript Instance
	{
		get
		{
			//check to see if it doesnt exist
			if (instance == null)
			{
				//lock access, if it is already locked, wait.
				lock (syncRoot)
				{
					//the instance could have been made between
					//checking and waiting for a lock to release.
					if (instance == null)
					{
						//create a new instance
						instance = new ObjectPoolManagerScript();
					}
				}
			}
			//return either the new instance or the already built one.
			return instance;
		}
	}

	/// <summary>
	/// Create a new object pool of the objects you wish to pool
	/// </summary>
	/// <param name="objToPool">The object you wish to pool.  The name property of the object MUST be unique.</param>
	/// <param name="initialPoolSize">Number of objects you wish to instantiate initially for the pool.</param>
	/// <param name="maxPoolSize">Maximum number of objects allowed to exist in this pool.</param>
	/// <returns></returns>
	public bool CreatePool(GameObject objToPool, int initialPoolSize, int maxPoolSize)
	{
		//Check to see if the pool already exists.
		if (ObjectPoolManagerScript.Instance.objectPools.ContainsKey(objToPool.name))
		{
			//let the caller know it already exists, just use the pool out there.
			return false;
		}
		else
		{
			//create a new pool using the properties
			ObjectPoolScript nPool = new ObjectPoolScript(objToPool, initialPoolSize, maxPoolSize);
			//Add the pool to the dictionary of pools to manage
			//using the object name as the key and the pool as the value.
			ObjectPoolManagerScript.Instance.objectPools.Add(objToPool.name, nPool);
			//We created a new pool!
			return true;
		}
	}

	/// <summary>
	/// Destroy an object pool of the objects you wish to destroy
	/// </summary>
	/// <param name="objToDestroy">The object pool you wish to destroy.  The name property of the object MUST be unique.</param>
	/// <returns></returns>
	public bool DestroyPool(GameObject objToDestroy)
	{
		//Check to see if the pool exists.
		if (!ObjectPoolManagerScript.Instance.objectPools.ContainsKey(objToDestroy.name))
		{
			//let the caller know it does not exists.
			return false;
		}
		else
		{
			//destroy all object based on the pool name
			ObjectPoolManagerScript.Instance.objectPools[objToDestroy.name].DestroyAllObjects();
			//remove the key from the object pool manager
			ObjectPoolManagerScript.Instance.objectPools.Remove(objToDestroy.name);
			//We destroy the pool!
			return true;
		}
	}

	/// <summary>
	/// Get an object from the pool.
	/// </summary>
	/// <param name="objName">String name of the object you wish to have access to.</param>
	/// <returns>A GameObject if one is available, else returns null if all are currently active and max size is reached.</returns>
	public GameObject GetObject(string objName)
	{
		//Find the right pool and ask it for an object.
		return ObjectPoolManagerScript.Instance.objectPools[objName].GetObject();
	}
}