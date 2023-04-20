using Microsoft.Win32;
using System;

namespace LOONACIA.Wpf.Controls;
public record FileDialogInfo(FileDialog Dialog, Func<FileDialog, object?> GetParameters);