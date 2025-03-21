using Microsoft.UI.Xaml;
//using Microsoft.UI.Xaml.Controls;
//using Microsoft.UI.Xaml.Controls.Primitives;
//using Microsoft.UI.Xaml.Data;
//using Microsoft.UI.Xaml.Input;
//using Microsoft.UI.Xaml.Media;
//using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Windows;
using Windows.UI.Popups;


// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class UnitsConverter : Page
	{
		public UnitsConverter()
		{
			this.InitializeComponent();
		}

		private async void btnConvert_Click(object sender, RoutedEventArgs e)
		{

			// Validate input
			if (!decimal.TryParse(quantityInputTextBox.Text, out decimal inputValue))
			{
				await new MessageDialog("Please enter a valid numeric value.", "Input Error").ShowAsync();
				return;
			}

			string selectedUnit = ((cmbUnits.SelectedItem as ComboBoxItem)?.Content?.ToString());
			if (string.IsNullOrEmpty(selectedUnit))
			{
				await new MessageDialog("Please select a unit to convert.", "Selection Error").ShowAsync();
				return;
			}

			decimal result = 0;
			string formula = "";
			string outputUnit = "";
			string resultFormat = ""; // Holds format specifier

			// Perform unit conversion
			switch (selectedUnit)
			{
				// result with 1 decimal place
				case "Celsius to Fahrenheit":
					result = (inputValue * 1.8M) + 32;
					formula = "F = (C × 1.8) + 32";
					outputUnit = "Fahrenheit";
					resultFormat = "F1"; // 1 decimal place
					break;

				case "Fahrenheit to Celsius":
					result = (inputValue - 32) / 1.8M;
					formula = "C = (F - 32) / 1.8";
					outputUnit = "Celsius";
					resultFormat = "F1";
					break;

				// result with 3 decimal places
				case "Meters to Feet":
					result = inputValue * 3.28084M;
					formula = "Feet = M × 3.28084";
					outputUnit = "Feet";
					resultFormat = "F3";
					break;

				case "Feet to Meters":
					result = inputValue / 3.28084M;
					formula = "M = Feet / 3.28084";
					outputUnit = "Meters";
					resultFormat = "F3";
					break;

				case "Kilograms to Pounds":
					result = inputValue * 2.20462M;
					formula = "Pounds = Kg × 2.20462";
					outputUnit = "Pounds";
					resultFormat = "F3";
					break;

				case "Pounds to Kilograms":
					result = inputValue / 2.20462M;
					formula = "Kg = Pounds / 2.20462";
					outputUnit = "Kilograms";
					resultFormat = "F3";
					break;

				// result with 4 decimal places
				case "Kpa to PSI":
					result = inputValue / 6.89476M;
					formula = "PSI = Kpa / 6.89476";
					outputUnit = "PSI";
					resultFormat = "F4";
					break;

				case "PSI to Kpa":
					result = inputValue * 6.89476M;
					formula = "Kpa = PSI × 6.89476";
					outputUnit = "Kpa";
					resultFormat = "F4";
					break;

				default:
					await new MessageDialog("Invalid selection. Please try again.", "Error").ShowAsync();
					return;
			}

			// Display formula and result with correct decimal places
			summaryTextBox.Text = $"{inputValue} {selectedUnit.Split(' ')[0]} = {result.ToString(resultFormat)} {outputUnit}.\n\nFormula used:\n{formula}";


		}

		private void btnExit_Click(object sender, RoutedEventArgs e)
		{
			Frame.Navigate(typeof(MainMenu)); // Navigate to MainPage.xaml
		}

		private void btnClear_Click(object sender, RoutedEventArgs e)
		{
			quantityInputTextBox.Text = "";

			// Clear the ComboBox selection
			cmbUnits.SelectedItem = null;  // Clears the selection

			summaryTextBox.Text = "Summary data displayed here";

		}

	}
}
