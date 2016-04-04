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

  1. Define a module and invoke the `notify_icon/2` macro:
    ```
    defmodule MyApp.MyIcon do
      import WinNotify
      @title "MyIcon"
      @icon Path.expand("../path/to/icon.ico", __DIR__)
      
      notify_icon @title, @icon
    end
    ```
      
  2. Add `MyIcon` to your applications supervision tree:  
    ```
    def start(_type, _args) do      
      import Supervisor.Spec, warn: false
      children = [
        worker(MyApp.MyIcon, []])
      ]
      opts = [strategy: :one_for_one, name: MyApp.Supervisor]
      Supervisor.start_link(children, opts)
    end
    ```
      
  3. Create balloop tip alerts:
    ```
    iex> MyApp.MyIcon.info("Info", "This is an info alert.")
    iex> MyApp.MyIcon.warning("Warning", "This is a warning alert.")
    iex> MyApp.MyIcon.error("Error", "This is an error alert.")
    ```