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
using static ExSharp.Runner;

[ExSharpModule("WinNotify.CSharp")]
public static class WinNotify 
{
  public static readonly IContainer Container = new Container();
  public static IDictionary<Reference, Icon> Icons = new Dictionary<Reference, Icon>();
    
  [ExSharpFunction("init_notify_icon", 1)]
  public static void InitNotifyIcon(ElixirTerm[] argv, int argc) 
  {
    var icon = new Icon();
    Icons.Add(ElixirTerm.GetReference(argv[0]), icon);
  }
  
  [ExSharpFunction("set_text", 2)]
  public static void SetText(ElixirTerm[] argv, int argc)
  {
    var reference = ElixirTerm.GetReference(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(reference, out icon))
    {
      throw new Exception("Icon not found");
    }

    icon.NotifyIcon.Text = ElixirTerm.GetUTF8String(argv[1]);
  }
  
  [ExSharpFunction("set_icon", 2)]
  public static void SetIcon(ElixirTerm[] argv, int argc)
  {
    var reference = ElixirTerm.GetReference(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(reference, out icon))
    {
      throw new Exception("Icon not found");
    }
    
    var path = ElixirTerm.GetUTF8String(argv[1]);
    icon.NotifyIcon.Icon = new System.Drawing.Icon(path);
  }
  
  [ExSharpFunction("toggle_visibility", 1)]
  public static void ToggleVisibility(ElixirTerm[] argv, int argc)
  {
    var reference = ElixirTerm.GetReference(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(reference, out icon))
    {
      throw new Exception("Icon not found");
    }
    
    icon.NotifyIcon.Visible = !icon.NotifyIcon.Visible;
  }
  
  [ExSharpFunction("show_balloon_tip", 4)]
  public static void ShowBalloonTip(ElixirTerm[] argv, int argc)
  {
    var reference = ElixirTerm.GetReference(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(reference, out icon))
    {
      throw new Exception("Icon not found");
    }
    
    var title = ElixirTerm.GetUTF8String(argv[1]);
    
    var text = ElixirTerm.GetUTF8String(argv[2]);
    
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
        throw new ArgumentException("Unknown tool tip icon type");
    }
    
    icon.NotifyIcon.ShowBalloonTip(0, title, text, toolTipIcon);
  }
  
  [ExSharpFunction("destroy_icon", 1)]
  public static void DestoryIcon(ElixirTerm[] argv, int argc)
  {
    var reference = ElixirTerm.GetReference(argv[0]);

    Icon icon;
    if (!Icons.TryGetValue(reference, out icon))
    {
      throw new Exception("Icon not found");
    }

    icon.Dispose();
    Icons.Remove(reference);
  }
  
  [ExSharpFunction("wipe_all_icons", 0)]
  public static void WipeAllIcons(ElixirTerm[] argv, int argc) 
  {
    Container.Dispose();
    Icons = new Dictionary<Reference, Icon>();
    return; 
  }
  
  [ExSharpFunction("icon_count", 0)]
  public static ElixirTerm IconCount(ElixirTerm[] argv, int argc)
  => ElixirTerm.MakeInt(Icons.Count());
  
  [ExSharpFunction("list_icons", 0)]
  public static ElixirTerm ListIcons(ElixirTerm[] argv, int argc) 
  {
    var items = Icons.Keys.Select(ElixirTerm.MakeRef)
      .Concat(new ElixirTerm[]{ ElixirTerm.MakeEmptyList() })
      .ToArray();
    
    return ElixirTerm.MakeList(items);
  }
}

public class Icon : IDisposable
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

var runner = new ExSharp.Runner();
await runner.Run();