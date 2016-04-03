defmodule WinNotify.Application do
  use Application
  @csx Path.expand("../../priv/WinNotify.csx", __DIR__)
  
  def start(_type, _args) do      
    import Supervisor.Spec, warn: false
    children = [
      worker(ExSharp.Roslyn, [@csx, [name: WinNotify.Roslyn]])
    ]
    opts = [strategy: :one_for_one, name: WinNotify.Supervisor]
    Supervisor.start_link(children, opts)
  end
end