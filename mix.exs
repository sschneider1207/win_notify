defmodule WinNotify.Mixfile do
  use Mix.Project

  def project do
    [app: :win_notify,
     version: "0.0.1",
     elixir: "~> 1.2",
     build_embedded: Mix.env == :prod,
     start_permanent: Mix.env == :prod,
     deps: deps]
  end

  def application do
    [applications: [:logger, :ex_sharp]]
  end
  
  defp deps do
    [{:ex_sharp, "~> 0.0.2"}]
  end
end
