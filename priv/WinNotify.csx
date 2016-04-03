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
    var hashCode = ElixirTerm.GetInt(argv[0]);
    if(!hashCode.HasValue) 
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Hash code must be an integer")
      });
    }

    Icon icon;
    if (!Icons.TryGetValue(hashCode.Value, out icon))
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Icon not found")
      });
    }

    icon.NotifyIcon.Text = ElixirTerm.GetUTF8String(argv[1]);
    return ElixirTerm.MakeAtom("ok");
  }
  
  [ExSharpFunction("set_icon", 2)]
  public static ElixirTerm SetIcon(ElixirTerm[] argv, int argc)
  {
    var hashCode = ElixirTerm.GetInt(argv[0]);
    if(!hashCode.HasValue) 
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Hash code must be an integer")
      });
    }
    
    Icon icon;
    if (!Icons.TryGetValue(hashCode.Value, out icon))
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Icon not found")
      });
    }

    try
    {
      var path = ElixirTerm.GetUTF8String(argv[1]);
      icon.NotifyIcon.Icon = new System.Drawing.Icon(path);
      return ElixirTerm.MakeAtom("ok");
    }
    catch (Exception e)
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String(e.Message)
      });
    }
  }
  
  [ExSharpFunction("toggle_visibility", 1)]
  public static ElixirTerm ToggleVisibility(ElixirTerm[] argv, int argc)
  {
    var hashCode = ElixirTerm.GetInt(argv[0]);
    if(!hashCode.HasValue) 
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Hash code must be an integer")
      });
    }
    
    Icon icon;
    if (!Icons.TryGetValue(hashCode.Value, out icon))
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Icon not found")
      });
    }
    
    icon.NotifyIcon.Visible = !icon.NotifyIcon.Visible;
    return ElixirTerm.MakeAtom("ok");
  }
  
  [ExSharpFunction("show_balloon_tip", 4)]
  public static ElixirTerm ShowBalloonTip(ElixirTerm[] argv, int argc)
  {
    var hashCode = ElixirTerm.GetInt(argv[0]);
    if(!hashCode.HasValue) 
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Hash code must be an integer")
      });
    }
    
    Icon icon;
    if (!Icons.TryGetValue(hashCode.Value, out icon))
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Icon not found")
      });
    }
    
    var title = ElixirTerm.GetUTF8String(argv[1]);
    if(string.IsNullOrWhiteSpace(title)) {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Title must be a byte string")
      });      
    }
    
    var text = ElixirTerm.GetUTF8String(argv[2]);
    if(string.IsNullOrWhiteSpace(title)) {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Text must be a byte string")
      });      
    }
    
    var toolTipIconAtom = ElixirTerm.GetAtom(argv[3]);
    ToolTipIcon toolTipIcon;
    switch(toolTipIconAtom) 
    {
      case "error":
        toolTipIcon = ToolTipIcon.Error;
        break;
      case "info":
        toolTipIcon = ToolTipIcon.Info;
        break;
      case "none":
        toolTipIcon = ToolTipIcon.None;
        break;
      case "warning":
        toolTipIcon = ToolTipIcon.Warning;
        break;
      default:
        return ElixirTerm.MakeTuple(new ElixirTerm[]{
          ElixirTerm.MakeAtom("error"),
          ElixirTerm.MakeUTF8String("Unknown tool tip icon type")
        }); 
    }
    
    icon.NotifyIcon.ShowBalloonTip(0, title, text, toolTipIcon);
    return ElixirTerm.MakeAtom("ok");
  }
  
  [ExSharpFunction("destroy_icon", 1)]
  public static ElixirTerm DestoryIcon(ElixirTerm[] argv, int argc)
  {
    var hashCode = ElixirTerm.GetInt(argv[0]);
    if(!hashCode.HasValue) 
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Hash code must be an integer")
      });
    }
    
    Icon icon;
    if (!Icons.TryGetValue(hashCode.Value, out icon))
    {
      return ElixirTerm.MakeTuple(new ElixirTerm[]{
        ElixirTerm.MakeAtom("error"),
        ElixirTerm.MakeUTF8String("Icon not found")
      });
    }

    icon.Dispose();
    Icons.Remove(hashCode.Value);
    return ElixirTerm.MakeAtom("ok");
  }
  
  [ExSharpFunction("wipe_all_icons", 0)]
  public static void WipeAllIcons(ElixirTerm[] argv, int argc) 
  {
    Container.Dispose();
    Icons = new Dictionary<int, Icon>();
    return; 
  }
  
  [ExSharpFunction("icon_count", 0)]
  public static ElixirTerm IconCount(ElixirTerm[] argv, int argc)
  => ElixirTerm.MakeInt(Icons.Count());
  
  [ExSharpFunction("list_icons", 0)]
  public static ElixirTerm ListIcons(ElixirTerm[] argv, int argc) 
  {
    var items = Icons.Keys.Select(ElixirTerm.MakeInt)
      .Concat(new ElixirTerm[]{ ElixirTerm.MakeEmptyList() })
      .ToArray();
    
    return ElixirTerm.MakeList(items);
  }
  
  class Icon : IDisposable
  {
    public NotifyIcon NotifyIcon { get; }
    public ContextMenu ContextMenu { get; }
    public List<MenuItem> MenuItems { get; }
    private readonly Guid _guid;

    public Icon()
    {
      NotifyIcon = new NotifyIcon(WinNotify.Container);
      ContextMenu = new ContextMenu();
      MenuItems = new List<MenuItem>();
      _guid = Guid.NewGuid();

      NotifyIcon.ContextMenu = ContextMenu;
      NotifyIcon.Visible = true;

    }

    public override int GetHashCode() => _guid.GetHashCode();
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
      return _guid.Equals(icon._guid);
    }

    public void Dispose() => NotifyIcon.Dispose();
  }
}
var runner = new ExSharp.Runner();
runner.Run();