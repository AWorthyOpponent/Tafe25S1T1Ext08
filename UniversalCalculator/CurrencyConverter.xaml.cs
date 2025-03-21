using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
	public sealed partial class CurrencyConverter : Page
	{
		private readonly Dictionary<string, Dictionary<string, double>> conversionRates;

		public CurrencyConverter()
		{
			this.InitializeComponent();

			// Initialize conversion rates
			conversionRates = new Dictionary<string, Dictionary<string, double>>()
			{
				{ "US Dollar", new Dictionary<string, double>()
					{ { "Euro", 0.85189982 }, { "British Pound", 0.72872436 }, { "Indian Rupee", 74.257327 } }
				},
				{ "Euro", new Dictionary<string, double>()
					{ { "US Dollar", 1.1739732 }, { "British Pound", 0.8556672 }, { "Indian Rupee", 87.00755 } }
				},
				{ "British Pound", new Dictionary<string, double>()
					{ { "US Dollar", 1.371907 }, { "Euro", 1.1686692 }, { "Indian Rupee", 101.68635 } }
				},
				{ "Indian Rupee", new Dictionary<string, double>()
					{ { "US Dollar", 0.011492628 }, { "Euro", 0.013492774 }, { "British Pound", 0.0098339397 } }
				}
			};

			// Bind currency options to the dropdown menus
			fromCurrencyComboBox.ItemsSource = conversionRates.Keys;
			toCurrencyComboBox.ItemsSource = conversionRates.Keys;
		}

		// Method to perform currency conversion
		private double ConvertCurrency(double amount, string fromCurrency, string toCurrency)
		{
			if (conversionRates.ContainsKey(fromCurrency) && conversionRates[fromCurrency].ContainsKey(toCurrency))
			{
				double rate = conversionRates[fromCurrency][toCurrency];
				return amount * rate;
			}
			return 0; // Return 0 if the conversion rate is not found
		}

		// Event handler for the Convert button click event
		private void ConvertButton_Click(object sender, RoutedEventArgs e)
		{
			if (double.TryParse(amountTextBox.Text, out double amount))
			{
				string fromCurrency = fromCurrencyComboBox.SelectedItem as string;
				string toCurrency = toCurrencyComboBox.SelectedItem as string;


				double convertedAmount = ConvertCurrency(amount, fromCurrency, toCurrency);
				double rate = conversionRates[fromCurrency][toCurrency];
				double reverseRate = conversionRates[toCurrency][fromCurrency];

				conversionResultText.Text = $"{amount} {fromCurrency} = {convertedAmount:F2} {toCurrency}";
				rateInfoText.Text = $"1 {fromCurrency} = {rate} {toCurrency}\n" +
					$"1 {toCurrency} = {reverseRate} {fromCurrency}";
			}

			else
			{
				conversionResultText.Text = "Please enter a valid amount.";
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Exit();
		}
	}
}