defmodule WinNotify.Mixfile do
  use Mix.Project

  def project do
    [app: :win_notify,
     version: "0.0.1",
     elixir: "~> 1.2",
     description: description,
     build_embedded: Mix.env == :prod,
     start_permanent: Mix.env == :prod,
     package: package,
     deps: deps]
  end

  def application do
    [applications: [:logger, :ex_sharp],
     mod: {WinNotify.Application, []}]
  end
  
  defp deps do
    [{:ex_sharp, "~> 0.0.3"},
     {:ex_doc, "~> 0.11.4", only: [:dev]},
     {:earmark, "~> 0.2.1", only: [:dev]} ]
  end
  
  defp description do
    """
    Manage Windows taskbar notification icons and send alerts.
    """
  end
  
  defp package do
    [
     maintainers: ["Sam Schneider"],
     licenses: ["MIT"],
     links: %{"GitHub" => "https://github.com/sschneider1207/win_notify"}]
  end
end
