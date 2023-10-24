# Window
A Window, which is managed by the [Window Manager](/docs/window-manager.md).

## Variables

### [`_IsVisible`](https://github.com/MrBisquit/Banana-OS-Basic-V2/blob/6ee077ef796f35d935c4d002baea007f1117ba38/Banana_OS_Basic_V2/Window/Window.cs#L17C9-L17C9): [bool]()
```csharp
static bool _IsVisible { get; set; } = true;
```

### [`_WindowName`](https://github.com/MrBisquit/Banana-OS-Basic-V2/blob/6ee077ef796f35d935c4d002baea007f1117ba38/Banana_OS_Basic_V2/Window/Window.cs#L18C6-L18C6): [string]()
```csharp
static string _WindowName { get; set; } = "";
```

### [`_Process`](https://github.com/MrBisquit/Banana-OS-Basic-V2/blob/6ee077ef796f35d935c4d002baea007f1117ba38/Banana_OS_Basic_V2/Window/Window.cs#L19): [Process]()
```csharp
static Process.Process _Process { get; set; }
```

### [`_CanClose`](): [bool]()
```csharp
static bool _CanClose { get; set; } = true;
```

### [`_WindowState`](): [WindowState]()
```csharp
static WindowState _WindowState { get; set; } = WindowState.Normal;
```

### [`CanMinimise`](): [bool]()
```csharp
public bool CanMinimize = true;
```

### [`CanMaximize`](): [bool]()
```csharp
public bool CanMaximize = true;
```

<!-- Template for variable
### [` `](): []()
```csharp

```-->
