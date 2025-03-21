using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MortgageCalculator : Page
    {
        public MortgageCalculator()
        {
            this.InitializeComponent();
        }

		private async void calculateButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			double annualInterestRate, durationYears, durationMonths, monthlyInterestRateDecimal;
			double principalAmount, totalMonths, monthlyRepayments;

			try
			{
				// Parse user inputs safely
				if (!double.TryParse(annualInterestTextBox.Text, out annualInterestRate) ||
					!double.TryParse(durationYearsTextBox.Text, out durationYears) ||
					!double.TryParse(durationMonthsTextBox.Text, out durationMonths) ||
					!double.TryParse(principalAmountTextBox.Text, out principalAmount))
				{
					throw new FormatException();
				}

				if (annualInterestRate < 0 || durationYears < 0 || durationMonths < 0 || principalAmount < 0)
				{
					throw new FormatException();
				}
			}
			catch (FormatException)
			{
				var errorMsg = new MessageDialog("Error: Please enter valid positive numbers for all fields.");
				await errorMsg.ShowAsync();
				return;
			}


			// Convert interest rate to decimal format
			monthlyInterestRateDecimal = (annualInterestRate / 100) / 12;

			// Total months of loan
			totalMonths = (durationYears * 12) + durationMonths;

			if (totalMonths == 0)
			{
				var errorMsg = new MessageDialog("Error: Loan duration must be greater than zero.");
				await errorMsg.ShowAsync();
				return;
			}

			// Calculate Monthly Repayment
			if (monthlyInterestRateDecimal == 0)
			{
				// If interest rate is 0, simple division
				monthlyRepayments = principalAmount / totalMonths;
			}
			else
			{
				double factor = Math.Pow(1 + monthlyInterestRateDecimal, totalMonths);
				monthlyRepayments = principalAmount * (monthlyInterestRateDecimal * factor) / (factor - 1);
			}

			// Display results
			monthlyInterestTextBox.Text = (monthlyInterestRateDecimal * 100).ToString("0.0000") + "%";
			monthlyRepaymentTextBox.Text = monthlyRepayments.ToString("C");

		}

		private void clearButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			// Clear all input fields
			principalAmountTextBox.Text = "";
			durationYearsTextBox.Text = "";
			durationMonthsTextBox.Text = "";
			annualInterestTextBox.Text = "";

			// Clear output fields
			monthlyInterestTextBox.Text = "";
			monthlyRepaymentTextBox.Text = "";
		}

		private void exitButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			// Close or navigate back to main menu

			Frame.Navigate(typeof(MainMenu));
		}
	}
}
