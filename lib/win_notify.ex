defmodule WinNotify do
  @moduledoc """
  Create and manage Windows notification icons.
  """
  
  @type exception :: {:exception, String.t}

  @doc """
  Creates a notification icon and returns a hash code to identify it by.
  
  ## Example
  
      iex> WinNotify.init_notify_icon
      iex> 12345
  """
  @spec init_notify_icon :: integer
  def init_notify_icon do
    raise "C# init_notify_icon/0 not implemented"
  end
  
  @doc """
  Sets the ToolTip text displayed when the mouse pointer rests on a notification area icon.
  
  ## Example
  
      iex> icon = WinNotify.init_notify_icon
      iex> WinNotify.set_text icon, "text"
      iex> :ok
  """
  @spec set_text(integer, String.t) :: :ok | {:error, String.t}
  def set_text(_icon, _text) do
    raise "C# set_text/2 not implemented"
  end
  
  @doc """
  Sets the current icon.  Requires an image with extension `.ico`.
  
  ## Example
  
      iex> icon = WinNotify.init_notify_icon
      iex> WinNotify.set_icon icon, "path/to/icon.ico"
      iex> :ok
  """
  @spec set_icon(integer, String.t) :: :ok | {:error, String.t} | exception
  def set_icon(_icon, _path) do
    raise "C# set_icon/2 not implemented"
  end
  
  @doc """
  Toggles a value indicating whether the icon is visible in the notification area of the taskbar.
  
  ## Example
  
      iex> icon = WinNotify.init_notify_icon
      iex> WinNotify.toggle_visibility icon
      iex> :ok
  """
  @spec toggle_visibility(integer) :: :ok | {:error, String.t}
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
  
      iex> icon = WinNotify.init_notify_icon
      iex> WinNotify.set_icon icon, ~S"path/to/icon.ico"
      iex> WinNotify.show_balloon_tip icon, "title", "text", :info
      iex> :ok
  """
  @spec show_balloon_tip(integer, String.t, String.t, atom) :: :ok | {:error, String.t}
  def show_balloon_tip(_icon, _title, _text, _type) do
    raise "C# show_balloon_tip/4 not implemented"
  end
  
  @doc """
  Releases all resources used by the notify icon.
  
  ## Example
  
      iex> icon = WinNotify.init_notify_icon
      iex> WinNotify.destroy_icon icon
      iex> :ok
  """
  @spec destroy_icon(integer) :: :ok | {:error, String.t}
  def destroy_icon(_icon) do
    raise "C# destroy_icon/1 not implemented"
  end
  
  @doc """
  Destroys all existing icons.
  
  ## Example
  
      iex> WinNotify.init_notify_icon
      iex> WinNotify.init_notify_icon
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
  
      iex> i1 = WinNotify.init_notify_icon
      iex> i2 = WinNotify.init_notify_icon
      iex> [i1, i2] = WinNotify.list_icons
  """
  @spec list_icons :: [integer]
  def list_icons do
    raise "C# list_icons/0 not implemented"
  end
end
