defmodule WinNotify do
  @levels ~w(info warning error)a
  
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
      
  Alternately, shortcuts can be defined for commonly used alerts:
    
      defmodule Foo do
        import WinNotify
        
        notify_icon "Foo", "/path/to/icon.ico" do
          defalert :bar, :warning, "Unexpected Result", "Got: ~f; Expected ~f;"
        end
      end
      
      iex> Foo.start_link
      iex> Foo.bar [5.1, 2.4] # (warning alert with text "Got: 5.1; Expected 2.4;")
      iex> :ok
  
  ## Defined Functions
  
  The following functions will be defined in the module the macro is invoked in:
  
    1. info(title, text)
    2. warning(title, text)
    3. error(title, text)
  """
  defmacro notify_icon(text, icon) do 
    add_notify_icon text, icon, do: [] 
  end
  defmacro notify_icon(text, icon, do: block) do
    add_notify_icon text, icon, do: block
  end
  
  defp add_notify_icon(text, icon, do: block) do
    quote do
      use GenServer
      import WinNotify.CSharp
      @ref_buf :erlang.term_to_binary(make_ref)
      
      def start_link do
        GenServer.start_link(__MODULE__, [:erlang.binary_to_term(@ref_buf)], name: __MODULE__)
      end     
      
      unquote(block)
      
      def info(title, text) do
        GenServer.cast(__MODULE__, {:info, title, text})
      end
      
      def warning(title, text) do
        GenServer.cast(__MODULE__, {:warning, title, text})
      end
      
      def error(title, text) do
        GenServer.cast(__MODULE__, {:error, title, text})
      end
      
      def none(title, text) do
        GenServer.cast(__MODULE__, {:none, title, text})
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
  
  defmacro defalert(name, level, title, format) when level in @levels do
    quote do
      def unquote(name)(args) when is_list(args) do
        text = 
          unquote(format)
          |> :io_lib.format(args)
          |> :erlang.list_to_bitstring()
        GenServer.cast(__MODULE__, {unquote(level), unquote(title), text})
      end
    end
  end
end
