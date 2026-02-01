using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using System.Collections;

namespace AvaloniaModernApp.Views;

public partial class DashboardView : UserControl
{
    public DashboardView()
    {
        InitializeComponent();
        HookNavigationEvents();
    }

    private void HookNavigationEvents()
    {
        var navView = this.FindControl<NavigationView>("NavView");
        if (navView == null)
            return;

        navView.SelectionChanged += (_, e) =>
            ProcessNavigation(e.SelectedItemContainer, e.SelectedItem);

        navView.ItemInvoked += (_, e) =>
            ProcessNavigation(e.InvokedItemContainer, e.InvokedItem);

        HookMenuItems(navView.MenuItems);
        HookMenuItems(navView.FooterMenuItems);
    }

    private void HookMenuItems(IEnumerable? items)
    {
        if (items == null)
            return;

        foreach (var item in items)
        {
            if (item is NavigationViewItem navItem)
            {
                navItem.PointerPressed -= OnMenuItemPointerPressed;
                navItem.PointerPressed += OnMenuItemPointerPressed;
            }
        }
    }

    private void OnMenuItemPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        ProcessNavigation(sender, sender);
    }

    private void ProcessNavigation(object? container, object? item)
    {
        var tag = ExtractTag(container, item);
        if (!string.IsNullOrEmpty(tag) && DataContext is ViewModels.DashboardViewModel vm)
        {
            vm.Navigate(tag);
        }
    }

    private static string? ExtractTag(object? container, object? item)
    {
        if (container is NavigationViewItem navItem && navItem.Tag is string tagFromContainer)
            return tagFromContainer;

        if (item is NavigationViewItem navItem2 && navItem2.Tag is string tagFromItem)
            return tagFromItem;

        if (item is string content)
            return content.Replace(" ", "");

        var reflected = TryGetTagViaReflection(container) ?? TryGetTagViaReflection(item);
        if (!string.IsNullOrEmpty(reflected))
            return reflected;

        return null;
    }

    private static string? TryGetTagViaReflection(object? target)
    {
        if (target == null)
            return null;

        var tagProp = target.GetType().GetProperty("Tag");
        if (tagProp != null)
        {
            var value = tagProp.GetValue(target);
            if (value is string tag)
                return tag;
        }

        var contentProp = target.GetType().GetProperty("Content");
        if (contentProp != null)
        {
            var value = contentProp.GetValue(target);
            if (value is string content)
                return content.Replace(" ", "");
        }

        return null;
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}
