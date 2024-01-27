using System;

namespace Hyperbar.Interop.Windows;

[Flags]
public enum WindowStyle
{
    Border = 0x800000,
    Caption = 0xC00000,
    Child = 0x40000000,
    ChildWindow = 0x40000000,
    ChildChildren = 0x2000000,
    ClipSiblings = 0x4000000,
    Disabled = 0x8000000,
    DlgFrame = 0x400000,
    Group = 0x20000,
    HScroll = 0x100000,
    Iconic = 0x20000000,
    Maximize = 0x1000000,
    MaximizeBox = 0x10000,
    Minimize = 0x20000000,
    MinimizeBox = 0x20000,
    Overlapped = 0,
    OverlappedWindow = 0xCF0000,
    SizeBox = 0x40000,
    SysMenu = 0x80000,
    TabStop = 0x10000,
    ThickFrame = 0x40000,
    Tiled = 0,
    TiledWindow = 0xCF0000,
    Visible = 0x10000000,
    VScroll = 0x200000
}
