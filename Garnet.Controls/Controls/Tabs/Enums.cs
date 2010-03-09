// ------------------------------------------------------------------------------
// Copyright (c) 2007-2009 Pyramid Software (xiy3x0@gmail.com)
//  $project:  Garnet (garnet-editor.google-code.com)
//  $license:  General Public License v3
// ------------------------------------------------------------------------------
namespace Pyramid.Garnet.Controls.Tabs
{
    /// <summary>
    /// Hit test result of <see cref="GarnetTabStrip"/>
    /// </summary>
    public enum HitTestResult
    {
        CloseButton,
        MenuGlyph,
        TabItem,
        None
    }
    
    /// <summary>
    /// Theme Type
    /// </summary>
    public enum ThemeTypes
    {
        WindowsXP,
        Office2000,
        Office2003
    }

    /// <summary>
    /// Indicates a change into TabStrip collection
    /// </summary>
    public enum GarnetTabStripItemChangeTypes
    {
        Added,
        Removed,
        Changed,
        SelectionChanged
    }
}
