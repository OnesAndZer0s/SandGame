using System.Text.RegularExpressions;

namespace Sandbox.Common
{
  public class ResourceLocation : IComparable<ResourceLocation>
  {
    public static readonly char NAMESPACE_SEPARATOR = ':';
    public static readonly char PATH_SEPARATOR = '/';
    public static readonly string DEFAULT_NAMESPACE = "core";

    public string Domain { get; private set; }

    public string Path { get; private set; }

    public ResourceLocation(string domain, string path)
    {
      this.Domain = domain;
      this.Path = path;
    }

    public ResourceLocation(string path)
    {
      var parts = path.Split(NAMESPACE_SEPARATOR);
      this.Domain = parts.Length == 1 ? DEFAULT_NAMESPACE : AssertValidDomain(parts[0], path);
      this.Path = AssertValidPath(this.Domain, parts.Length == 1 ? parts[0] : parts[1]);
    }

    private static string AssertValidDomain(string domain, string path)
    {
      if (domain == null || domain.Length == 0)
      {
        throw new System.ArgumentException("Invalid resource domain: '" + domain + "' in path: '" + path + "'");
      }
      else if (!Regex.IsMatch(domain, @"^[a-zA-Z]+$"))
      {
        throw new System.ArgumentException("Invalid resource domain: '" + domain + "' in path: '" + path + "'");
      }
      else
      {
        return domain;
      }
    }

    private static string AssertValidPath(string domain, string path)
    {
      if (path == null || path.Length == 0)
      {
        throw new System.ArgumentException("Invalid resource path: '" + path + "'");
      }
      else if (!Regex.IsMatch(path, @"^[a-zA-Z_\/]+$"))
      {
        throw new System.ArgumentException("Invalid resource path: '" + path + "' in path: '" + path + "'");
      }
      else
      {
        return path;
      }
    }

    public int CompareDomain(ResourceLocation other)
    {
      int i = this.Domain.CompareTo(other.Domain);
      if (i == 0)
      {
        i = this.Path.CompareTo(other.Path);
      }
      return i;
    }

    public int CompareTo(ResourceLocation? other)
    {
      return this.Path.CompareTo(other!.Path);
    }

    public bool Equals(string domain, string path)
    {
      return this.Domain.Equals(domain) && this.Path.Equals(path);
    }

    public bool Equals(ResourceLocation other)
    {
      return this.Domain.Equals(other.Domain) && this.Path.Equals(other.Path);
    }

    public static bool IsAllowedInResourceLocation(string path)
    {
      return path.IndexOf(NAMESPACE_SEPARATOR) == -1 && path.IndexOf(PATH_SEPARATOR) == -1;
    }

    public static bool IsValidDomain(string domain)
    {
      if (domain == null || domain.Length == 0)
      {
        return false;
      }
      else if (!Regex.IsMatch(domain, @"^[a-zA-Z]+$"))
      {
        return false;
      }
      else
      {
        return true;
      }
    }

    public static bool IsValidPath(string path)
    {
      if (path == null || path.Length == 0)
      {
        return false;
      }
      else if (!Regex.IsMatch(path, @"^[a-zA-Z]+$"))
      {
        return false;
      }
      return true;
    }

    public static bool IsValidResourceLocation(string rS)
    {
      if (rS == null || rS.Length == 0)
      {
        return false;
      }
      else
      {
        string[] astring = rS.Split(NAMESPACE_SEPARATOR);
        if (astring.Length == 2)
        {
          string s = astring[0];
          string s1 = astring[1];
          return IsValidDomain(s) && IsValidPath(s1);
        }
        else
        {
          return false;
        }
      }
    }

    public static ResourceLocation Of(string domain, string path)
    {
      return new ResourceLocation(AssertValidDomain(domain, path), AssertValidPath(domain, path));
    }

    public string ToDebugFileName()
    {
      return this.Path.Replace(PATH_SEPARATOR, '_');
    }

    public string ToLanguageKey()
    {
      return this.Path.Replace(PATH_SEPARATOR, '.');
    }

    public string ToLanguageKey(string suffix)
    {
      return this.Path.Replace(PATH_SEPARATOR, '.') + '.' + suffix;
    }

    public override string ToString()
    {
      return this.Domain + NAMESPACE_SEPARATOR + this.Path;
    }

    public static ResourceLocation? TryParse(string rS)
    {
      if (rS == null || rS.Length == 0)
      {
        return null;
      }
      else
      {
        string[] astring = rS.Split(NAMESPACE_SEPARATOR);
        if (astring.Length == 2)
        {
          string s = astring[0];
          string s1 = astring[1];
          return new ResourceLocation(s, s1);
        }
        else
        {
          return null;
        }
      }
    }

    public static bool ValidNamespaceChar(char c)
    {
      return c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c == '_' || c == '-';
    }

    public static bool ValidPathChar(char c)
    {
      return c >= 'a' && c <= 'z' || c >= '0' && c <= '9' || c == '_' || c == '-' || c == '.';
    }

    public ResourceLocation WithPath(string path)
    {
      return new ResourceLocation(this.Domain, path);
    }

    public ResourceLocation WithPrefix(string prefix)
    {
      return new ResourceLocation(this.Domain, prefix + PATH_SEPARATOR + this.Path);
    }

    // cast string to ResourceLocation
    public static implicit operator ResourceLocation(string rS)
    {
      return new ResourceLocation(rS);
    }

    public override bool Equals(object? obj)
    {
      if (obj == null)
      {
        return false;
      }
      else if (this == (ResourceLocation)obj)
      {
        return true;
      }
      else if (!(obj is ResourceLocation))
      {
        return false;
      }
      else
      {
        ResourceLocation q = (ResourceLocation)obj;
        return this.Domain.Equals(q.Domain) && this.Path.Equals(q.Path);
      }
      // ResourceLocation q = obj as ResourceLocation;
      // return q.Domain == this.Domain && q.Path == this.Path;
    }

    public override int GetHashCode()
    {
      return this.Domain.GetHashCode() ^ this.Path.GetHashCode();
    }

    // implicit convert string to ResourceLocation
    public static implicit operator string(ResourceLocation rL)
    {
      return rL.ToString();
    }

    // implicit equals
    public static bool operator ==(ResourceLocation rL1, ResourceLocation rL2)
    {
      return rL1?.Domain == rL2?.Domain && rL1?.Path == rL2?.Path;
    }

    // implicit not equals
    public static bool operator !=(ResourceLocation rL1, ResourceLocation rL2)
    {
      return !(rL1 == rL2);
    }

  }
}