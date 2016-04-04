defmodule WinNotify do
  @doc """
  Defines a notification icon process.
  
  ## Example
  
      defmodule Foo do
        import WinNotify
        
        notify_icon "Foo", "/path/to/icon.ico"
      end
      
      iex> Foo.start_link
      iex> Foo.info("Info Event", "From Elixir!")
      iex> :ok
  
  ## Defined Functions
  
  The following functions will be defined in the module the macro is invoked in:
  
    1. info(title, text)
    2. warning(title, text)
    3. error(title, text)
  """
  defmacro notify_icon(text, icon) do
    quote do
      use GenServer
      import WinNotify.CSharp
      @ref_buf :erlang.term_to_binary(make_ref)
      
      def start_link do
        GenServer.start_link(__MODULE__, [:erlang.binary_to_term(@ref_buf)], name: __MODULE__)
      end
      
      def info(title, text) do
        GenServer.cast(__MODULE__, {:info, title, text})
      end
      
      def warning(title, text) do
        GenServer.cast(__MODULE__, {:warning, title, text})
      end
      
      def error(title, text) do
        GenServer.cast(__MODULE__, {:error, title, text})
      end
      
      def init([ref]) do
        init_notify_icon ref
        set_text ref, unquote(text)
        set_icon ref, unquote(icon)
        {:ok, ref}
      end
      
      def handle_cast({level, title, text}, ref) do
        show_balloon_tip(ref, title, text, level)
        {:noreply, ref}
      end
    end
  end
end
