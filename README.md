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

