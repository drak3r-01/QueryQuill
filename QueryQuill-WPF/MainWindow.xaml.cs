using QueryQuill_Lib;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace QueryQuill_WPF
{
    /// <summary>
    /// Partial class containing the code-behind of the <see cref="MainWindow"/>
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _dorkUniqueKey = 1;
        private readonly DorkParams _dorkParams = new();
        private Dork _dork = new(null);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            langOption_En.IsChecked = true;
            dorksList.SelectedIndex = 0;
            return;
        }

        /// <summary>
        /// Exit method of the application.
        /// </summary>
        private void AppExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
            return;
        }

        /// <summary>
        /// Method changing the display of the parameter bar according to the selected dork.
        /// </summary>
        private void DorksListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dorkParamsBar.Children.Clear();
            dorkParamsBar.ColumnDefinitions.Clear();
            dorkParamsBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });

            ComboBoxItem selectedDork = (ComboBoxItem)dorksList.SelectedItem;

            switch (selectedDork.Content.ToString())
            {
                case "daterange:":
                    dorkParamsBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(2, GridUnitType.Auto) });
                    dorkParamsBar.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

                    DatePicker dateRangeStartInput = new()
                    {
                        Name = "dateRangeStartInput",
                        SelectedDate = DateTime.Today,
                        SelectedDateFormat = DatePickerFormat.Short
                    };
                    DatePicker dateRangeEndInput = new()
                    {
                        Name = "dateRangeEndInput",
                        SelectedDate = DateTime.Today,
                        SelectedDateFormat = DatePickerFormat.Short
                    };
                    TextBlock toTextBox = new()
                    {
                        Text = "To",
                        Margin = new Thickness(10, 0, 10, 0),
                        TextAlignment = TextAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };

                    Grid.SetColumn(dateRangeStartInput, 0);
                    Grid.SetColumn(toTextBox, 1);
                    Grid.SetColumn(dateRangeEndInput, 2);

                    dorkParamsBar.Children.Add(dateRangeStartInput);
                    dorkParamsBar.Children.Add(toTextBox);
                    dorkParamsBar.Children.Add(dateRangeEndInput);
                    break;

                case "before:":
                case "after:":
                    DatePicker dateInput = new()
                    {
                        Name = "dateInput",
                        SelectedDate = DateTime.Today,
                        SelectedDateFormat = DatePickerFormat.Short
                    };

                    Grid.SetColumn(dateInput, 0);
                    dorkParamsBar.Children.Add(dateInput);
                    break;

                default:
                    TextBox paramValueInput = new() { Name = "paramValueInput" };

                    Grid.SetColumn(paramValueInput, 0);
                    dorkParamsBar.Children.Add(paramValueInput);
                    break;
            }

            return;
        }

        /// <summary>
        /// Method adding a dork parameter to the list of active parameters (visually and functionally)
        /// </summary>
        private void AddNewDorkParam(object sender, RoutedEventArgs e)
        {
            DockPanel newDorkListItem = new() { LastChildFill = true, Margin = new(5), Name = $"dorkUniqueKey_{_dorkUniqueKey}" };

            ComboBoxItem selectedDork = (ComboBoxItem)dorksList.SelectedItem;
            string selectedDorkName = selectedDork.Content.ToString().Replace(":", "");
            string dorkParamStringDescription;
            switch (selectedDorkName)
            {
                case "daterange":
                    DatePicker dateRangeStartInput = LogicalTreeHelper.FindLogicalNode(dorkParamsBar, "dateRangeStartInput") as DatePicker;
                    DateTime dateRangeStart = dateRangeStartInput.SelectedDate.Value;
                    DatePicker dateRangeEndInput = LogicalTreeHelper.FindLogicalNode(dorkParamsBar, "dateRangeEndInput") as DatePicker;
                    DateTime dateRangeEnd = dateRangeEndInput.SelectedDate.Value;

                    _dorkParams.AddParam(
                        [
                            selectedDorkName,
                            $"{dateRangeStart.Year}-{dateRangeStart.Month}-{dateRangeStart.Day}",
                            $"{dateRangeEnd.Year}-{dateRangeEnd.Month}-{dateRangeEnd.Day}"
                        ],
                        _dorkUniqueKey
                    );

                    dorkParamStringDescription = $"{dateRangeStart.Year}-{dateRangeStart.Month}-{dateRangeStart.Day} To {dateRangeEnd.Year}-{dateRangeEnd.Month}-{dateRangeEnd.Day}";
                    break;

                case "before":
                case "after":
                    DatePicker dateInput = LogicalTreeHelper.FindLogicalNode(dorkParamsBar, "dateInput") as DatePicker;
                    DateTime date = dateInput.SelectedDate.Value;

                    _dorkParams.AddParam(
                        [
                            selectedDorkName,
                            $"{date.Year}-{date.Month}-{date.Day}"
                        ],
                        _dorkUniqueKey
                    );

                    dorkParamStringDescription = $"{date.Year}-{date.Month}-{date.Day}";
                    break;

                default:
                    TextBox paramValueInput = LogicalTreeHelper.FindLogicalNode(dorkParamsBar, "paramValueInput") as TextBox;
                    _dorkParams.AddParam(
                        [
                            selectedDorkName switch {
                                "@" => "at",
                                "#" => "hash",
                                _ => selectedDorkName
                                },
                            paramValueInput.Text
                        ],
                        _dorkUniqueKey
                    );

                    dorkParamStringDescription = paramValueInput.Text;
                    break;
            }

            TextBlock dorkItemName = new()
            {
                Width = 75,
                VerticalAlignment = VerticalAlignment.Center,
                Text = selectedDorkName + ":",
                Margin = new(5, 0, 5, 0),
                Padding = new(5)
            };
            DockPanel.SetDock(dorkItemName, Dock.Left);
            newDorkListItem.Children.Add(dorkItemName);

            Button dorkItemRemoveButton = new() { Content = "Remove", Margin = new(5, 0, 5, 0), Padding = new(5) };
            dorkItemRemoveButton.Click += (sender, e) =>
            {
                Button senderRemoveButton = sender as Button;
                DockPanel dorkItem = LogicalTreeHelper.GetParent(senderRemoveButton) as DockPanel;

                _dorkParams.RemoveParam(int.Parse(dorkItem.Name.Replace("dorkUniqueKey_", "")));

                dorksListActive.Children.RemoveAt(dorksListActive.Children.IndexOf(dorkItem) + 1);
                dorksListActive.Children.Remove(dorkItem);
                GenerateDork();
            };
            DockPanel.SetDock(dorkItemRemoveButton, Dock.Right);
            newDorkListItem.Children.Add(dorkItemRemoveButton);

            newDorkListItem.Children.Add(new TextBox()
            {
                IsReadOnly = true,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                Padding = new(5),
                Text = dorkParamStringDescription
            });

            dorksListActive.Children.Add(newDorkListItem);

            Separator dorkListItemSeparator = new() { Style = (Style)Application.Current.Resources[ToolBar.SeparatorStyleKey] };
            dorksListActive.Children.Add(dorkListItemSeparator);

            _dorkUniqueKey++;
            GenerateDork();
            return;
        }

        /// <summary>
        /// Method generating the dork from the active parameters.
        /// </summary>
        private void GenerateDork()
        {
            _dork = DorkGenerator.GenerateDork(_dorkParams);
            outputArea.Text = _dork.Query;
            return;
        }

        /// <summary>
        /// Method opening the last generated dork in the default web browser.
        /// </summary>
        private void OpenInBrowser(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo() { FileName = _dork.Link.ToString(), UseShellExecute = true });
        }

        /// <summary>
        /// Method copying the query of the last generated dork to the clipboard.
        /// </summary>
        private void CopyIntoClipBoard(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(_dork.Query);
        }
    }
}