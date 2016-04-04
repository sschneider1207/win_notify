defmodule WinNotify.CSharp do
  @moduledoc """
  Create and manage Windows notification icons.
  """
  
  @type exception :: {:exception, String.t}

  @doc """
  Creates a notification icon associated with reference.
  
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> :ok
  """
  @spec init_notify_icon(reference) :: :ok
  def init_notify_icon(reference) do
    raise "C# init_notify_icon/0 not implemented"
  end
  
  @doc """
  Sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.
  
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> WinNotify.set_text ref, "text"
      iex> :ok
  """
  @spec set_text(reference, String.t) :: :ok
  def set_text(_icon, _text) do
    raise "C# set_text/2 not implemented"
  end
  
  @doc """
  Sets the current icon.  Requires an image with extension `.ico`.
  
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> WinNotify.set_icon ref, "path/to/icon.ico"
      iex> :ok
  """
  @spec set_icon(reference, String.t) :: :ok
  def set_icon(_icon, _path) do
    raise "C# set_icon/2 not implemented"
  end
  
  @doc """
  Toggles a value indicating whether the icon is visible in the notification area of the taskbar.
  
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> WinNotify.toggle_visibility ref
      iex> :ok
  """
  @spec toggle_visibility(reference) :: :ok
  def toggle_visibility(_icon) do
    raise "C# toggle_visibility/1 not implemented"
  end
  
  @doc """
  Displays a balloon tip with the specified title, text, and icon in the taskbar for the specified time period.
  Notify icon must be visible and be set with an icon image.
  
  ## Icon types
  
    * `:error`
    * `:info`
    * `:none`
    * `:warning`
    
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> WinNotify.set_icon ref, ~S"path/to/icon.ico"
      iex> WinNotify.show_balloon_tip ref, "title", "text", :info
      iex> :ok
  """
  @spec show_balloon_tip(reference, String.t, String.t, atom) :: :ok
  def show_balloon_tip(_icon, _title, _text, _type) do
    raise "C# show_balloon_tip/4 not implemented"
  end
  
  @doc """
  Releases all resources used by the notify icon.
  
  ## Example
  
      iex> ref = make_ref
      iex> WinNotify.init_notify_icon ref
      iex> WinNotify.destroy_icon ref
      iex> :ok
  """
  @spec destroy_icon(reference) :: :ok
  def destroy_icon(_icon) do
    raise "C# destroy_icon/1 not implemented"
  end
  
  @doc """
  Destroys all existing icons.
  
  ## Example
  
      iex> WinNotify.init_notify_icon make_ref
      iex> WinNotify.init_notify_icon make_ref
      iex> WinNotify.wipe_all_icons
      iex> :ok
  """
  @spec wipe_all_icons :: :ok
  def wipe_all_icons do
    raise "C# wipe_all_icons/0 not implemented"
  end
  
  @doc """
  Current number of existing icons
  
  ## Example
  
      iex> WinNotify.init_notify_icon
      iex> WinNotify.init_notify_icon
      iex> WinNotify.icon_count
      iex> 2
  """
  @spec icon_count :: integer
  def icon_count do
    raise "C# icon_count/0 not implemented"
  end
  
  @doc """
  List of all existing icons hash codes
  
  ## Example
  
      iex> ref1 = make_ref
      iex> ref2 = make_ref
      iex> WinNotify.init_notify_icon ref1
      iex> WinNotify.init_notify_icon ref2
      iex> [i1, i2] = WinNotify.list_icons
  """
  @spec list_icons :: [reference]
  def list_icons do
    raise "C# list_icons/0 not implemented"
  end
end