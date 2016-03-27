#r "System.Windows.Forms"
#r "System.Drawing"

using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.IO;
using System.Text;
using ExSharp;

[ExSharpModule("WinNotify")]
public static class WinNotify 
{
  private static readonly IContainer Container = new Container();
  private static IDictionary<int, Icon> Icons = new Dictionary<int, Icon>();
    
  [ExSharpFunction("init_notify_icon", 0)]
  public static ElixirTerm InitNotifyIcon(ElixirTerm[] argv, int argc) 
  {
    var icon = new Icon();
    Icons.Add(icon.GetHashCode(), icon);
    return ElixirTerm.MakeInt(icon.GetHashCode());
  }
  
  [ExSharpFunction("set_text", 2)]
  public static ElixirTerm SetText(ElixirTerm[] argv, int argc)
  {
    int hashCode = ElixirTerm.GetInt(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(hashCode, out icon))
    {
      return ElixirTerm.MakeAtom("error");
    }

    icon.NotifyIcon.Text = ElixirTerm.GetUTF8String(argv[1]);
    return ElixirTerm.MakeAtom("ok");
  }
  
  [ExSharpFunction("set_icon", 2)]
  public static ElixirTerm SetIcon(ElixirTerm[] argv, int argc)
  {
    int hashCode = ElixirTerm.GetInt(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(hashCode, out icon))
    {
      return ElixirTerm.MakeAtom("error");
    }

    try
    {
      var path = ElixirTerm.GetUTF8String(argv[1]);
      icon.NotifyIcon.Icon = new System.Drawing.Icon(path);
      return ElixirTerm.MakeAtom("ok");
    }
    catch (Exception e)
    {
      return ElixirTerm.MakeAtom("error");
    }
  }
  
  [ExSharpFunction("destory_icon", 1)]
  public static ElixirTerm DestoryIcon(ElixirTerm[] argv, int argc)
  {
    int hashCode = ElixirTerm.GetInt(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(hashCode, out icon))
    {
      return ElixirTerm.MakeAtom("error");
    }

    icon.Dispose();
    Icons.Remove(hashCode);
    return ElixirTerm.MakeAtom("ok");
  }
  
  class Icon : IDisposable
  {
    public NotifyIcon NotifyIcon { get; }
    public ContextMenu ContextMenu { get; }
    public List<MenuItem> MenuItems { get; }
    public Guid Guid { get; }

    public Icon()
    {
      NotifyIcon = new NotifyIcon(WinNotify.Container);
      ContextMenu = new ContextMenu();
      MenuItems = new List<MenuItem>();
      Guid = Guid.NewGuid();

      NotifyIcon.ContextMenu = ContextMenu;
      NotifyIcon.Visible = true;

    }

    public override int GetHashCode() => Guid.GetHashCode();
    public override bool Equals(object obj)
    {
      if (obj == null)
      {
        return false;
      }

      if (!(obj is Icon))
      {
        return false;
      }
      var icon = (Icon)obj;
      return Guid.Equals(icon.Guid);
    }

    public void Dispose() => NotifyIcon.Dispose();
  }
}
var runner = new ExSharp.Runner();
runner.Run();