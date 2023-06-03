using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Sandbox.Common.Registry
{

  public interface IRegistry
  {
    void Freeze();
  }

  public class Registry<T> : Dictionary<ResourceLocation, T>, IRegistry
  {
    private bool frozen;
    private ResourceLocation name;

    public Registry(ResourceLocation name)
    {
      this.name = name;
      this.frozen = false;
    }

    public T Register(ResourceLocation? p_255878_, T p_255879_)
    {
      if (this.frozen)
      {
        throw new Exception("Unable to add new registry contents after registry is frozen.");
      }
      else if (p_255879_ == null)
      {
        throw new Exception("Can't register null object");
      }
      else
      {
        if (ContainsKey(p_255878_!))
        {
          throw new Exception("Duplicate registration " + p_255878_!);
        }
        Debug.Print("Registering " + p_255878_! + " " + p_255879_);
        Add(p_255878_!, p_255879_);
        return p_255879_;
      }
    }
    public ResourceLocation? GetKey(T value)
    {
      foreach (var item in this)
      {
        if (item.Value!.Equals(value))
        {
          return item.Key;
        }
      }
      return null;
    }
    public void Freeze()
    {
      this.frozen = true;
    }


    public T ByID(int id)
    {
      // TODO this has to be better
      return this.Values.ToList()[id];
    }

    public int GetID(T thing)
    {
      // TODO this has to be better

      return this.Values.ToList().IndexOf(thing);
    }
  }
}