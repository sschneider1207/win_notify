# WinNotify

Manage Windows notification icons with Elixir

## Installation

If [available in Hex](https://hex.pm/docs/publish), the package can be installed as:

  1. Add win_notify to your list of dependencies in `mix.exs`:

        def deps do
          [{:win_notify, "~> 0.0.1"}]
        end

  2. Ensure win_notify is started before your application:

        def application do
          [applications: [:win_notify]]
        end

## Usage

  1. Define a module and invoke the `notify_icon/2` macro.
      Optionally you can use `defalert/3` to create shortcuts for an alert you expect to use a lot:
    ```
    defmodule MyIcon do
      import WinNotify
      @icon Path.expand("../path/to/icon.ico", __DIR__)
      
      notify_icon "My Icon", @icon do      
        defalert :welcome, :info, "Welcome ~s!"
        defalert :unexpected, :warning, "Expected value to be ~f, but got ~f"
        defalert :not_found, :error, "~s not found"
      end
    end
    ```
      
  2. Add `MyIcon` to your applications supervision tree:  
    ```
    def start(_type, _args) do      
      import Supervisor.Spec, warn: false
      children = [
        worker(MyIcon, []])
      ]
      opts = [strategy: :one_for_one, name: MyIcon.Supervisor]
      Supervisor.start_link(children, opts)
    end
    ```
      
  3. Create balloop tip alerts or use alert shortcuts:
    ```
    iex> MyIcon.info("Info", "This is an info alert.")
    iex> MyIcon.warning("Warning", "This is a warning alert.")
    iex> MyIcon.error("Error", "This is an error alert.")
    iex> MyIcon.not_found(["Video"])
    iex> MyIcon.welcome([user.name])
    ```