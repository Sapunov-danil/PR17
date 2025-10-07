using PrintManagementSystem_Sapunov.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintManagementSystem_Sapunov
{
    public partial class MainWindow : Window
    {
        public List<TypeOpertation> typeOpertationList = TypeOpertation.AllTypeOperation();
        public List<Format> formatsList = Format.AllFormats();
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        void LoadData() 
        {
            foreach (TypeOpertation items in typeOpertationList)
                typeOperation.Items.Add(items.name);
            foreach (Format item in formatsList)
                formats.Items.Add(item.format);
        }

        public void CostCalculations() 
        {
            float price = 0;

            if (typeOperation.SelectedIndex != -1) 
            {
                if (typeOperation.SelectedItem as String == "Сканирование") price = 10;
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    if (formats.SelectedItem as String == "А4")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 4;
                                else price = 3;
                            }
                            else
                                price = 20;
                        }
                        else
                        {
                            if (Colors.IsChecked == false)
                            {
                                if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                    price = 6;
                                else price = 4;
                            }
                            else
                                price = 35;
                        }
                    }
                    else if (formats.SelectedItem as String == "А3")
                    {
                        if (TwoSides.IsChecked == false)
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 8;
                            else price = 6;
                        }
                        else
                        {
                            if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 30)
                                price = 12;
                            else price = 10;
                        }
                    }
                    else if (formats.SelectedItem as String == "А2")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 35;
                            else
                                price = 50;
                        }
                        else
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 120;
                            else
                                price = 170;
                        }
                    }
                    else if (formats.SelectedItem as String == "А1")
                    {
                        if (Colors.IsChecked == false)
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 75;
                            else
                                price = 120;
                        }
                        else
                        {
                            if (LotOfColor.IsChecked == false)
                                price = 170;
                            else
                                price = 250;
                        }
                    }
                }
                else if (typeOperation.SelectedItem as String == "Ризограф") 
                {
                    if (TwoSides.IsChecked == false) 
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.40f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.10f;
                        else
                            price = 1;
                    }
                    else 
                    {
                        if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 100)
                            price = 1.80f;
                        else if (textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) < 200 && textBoxCount.Text.Length > 0 && int.Parse(textBoxCount.Text) >= 100)
                            price = 1.40f;
                        else
                            price = 1.10f;
                    }
                }
            }
            if (textBoxCount.Text.Length > 0) 
            {
                textBoxPrice.Text = (float.Parse(textBoxCount.Text) * price).ToString();
            }
        }

        public void CalculationsAllPrice() 
        {
            float allPrice = 0;
            for (int i = 0; i < Operations.Items.Count; i++) 
            {
                TypeOperationsWindow newTOW = Operations.Items[i] as TypeOperationsWindow;
                allPrice += newTOW.price;
            }
            labelAllPrice.Content = $"Общая сумма: {allPrice}";
        }

        private void DeleteOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
                CalculationsAllPrice();
            }
            else 
            {
                MessageBox.Show("Пожалуйста, выберите операцию для удаления");
            }
        }

        private void EditOperation(object sender, RoutedEventArgs e)
        {
            if (Operations.SelectedIndex != -1)
            {
                TypeOperationsWindow newTow = Operations.Items[Operations.SelectedIndex] as TypeOperationsWindow;

                typeOperation.SelectedItem = typeOpertationList.Find(x => x.id == newTow.typeOperation).name;
                formats.SelectedItem = formatsList.Find(x => x.id == newTow.format).format;

                if (newTow.side == 1) TwoSides.IsChecked = false;
                else if (newTow.side == 2) TwoSides.IsChecked = true;

                Colors.IsChecked = newTow.color;

                string[] resultColor = newTow.colorText.Split('(');
                if (resultColor.Length == 1) LotOfColor.IsChecked = false;
                else if (resultColor.Length == 2) LotOfColor.IsChecked = true;

                textBoxCount.Text = newTow.count.ToString();
                textBoxPrice.Text = newTow.price.ToString();

                addOperationButton.Content = "Изменить";

                Operations.Items.Remove(Operations.Items[Operations.SelectedIndex]);
            }
            else MessageBox.Show("Пожалуйста, выберите операцию для редактирования");
        }

        private void AddOperation(object sender, RoutedEventArgs e)
        {
            TypeOperationsWindow newTow = new TypeOperationsWindow();
            newTow.typeOperationText = typeOperation.SelectedItem as String;
            newTow.typeOperation = typeOpertationList.Find(x => x.name == newTow.typeOperationText).id;

            if (formats.SelectedIndex != -1) 
            {
                newTow.formatText = formats.SelectedItem as String;
                newTow.format = formatsList.Find(x => x.format == newTow.formatText).id;
            }
            if (TwoSides.IsEnabled == true) 
            {
                if (TwoSides.IsChecked == false)
                    newTow.side = 1;
                else
                    newTow.side = 2;
            }
            if (Colors.IsChecked == false)
            {
                newTow.colorText = "Ч/Б";
                newTow.color = false;

                if (LotOfColor.IsChecked == true)
                {
                    newTow.colorText += "(> 50%)";
                    newTow.occupancy = true;
                }
            }
            else 
            {
                newTow.colorText = "ЦВ";
                newTow.color = true;

                if (LotOfColor.IsChecked == true) 
                {
                    newTow.colorText += "(> 50%)";
                    newTow.occupancy = true;
                }
            }
            newTow.count = int.Parse(textBoxCount.Text);
            newTow.price = float.Parse(textBoxPrice.Text);
            addOperationButton.Content = "Добавить";
            Operations.Items.Add(newTow);
            CalculationsAllPrice();
        }

        private void ColorsChange(object sender, RoutedEventArgs e)
        {
            CostCalculations();
        }

        private void textBoxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CostCalculations();
        }

        private void SelectedFormat(object sender, SelectionChangedEventArgs e)
        {
            if (formats.SelectedItem as String == "А4")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = false;
            }
            if (formats.SelectedItem as String == "А3")
            {
                TwoSides.IsEnabled = true;
                Colors.IsEnabled = false;
                LotOfColor.IsEnabled = false;
            }
            if (formats.SelectedItem as String == "А2" || formats.SelectedItem as String == "А1")
            {
                TwoSides.IsEnabled = false;
                Colors.IsEnabled = true;
                LotOfColor.IsEnabled = true;
            }

            if (textBoxCount.Text.Length == 0)
                textBoxCount.Text = "1";

            CostCalculations();
        }

        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            if (typeOperation.SelectedIndex != -1)
            {
                if (typeOperation.SelectedItem as String == "Сканирование")
                {
                    formats.SelectedIndex = -1;
                    TwoSides.IsChecked = false;
                    Colors.IsChecked = false;
                    LotOfColor.IsChecked = false;

                    formats.IsEnabled = false;
                    TwoSides.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                else if (typeOperation.SelectedItem as String == "Печать" || typeOperation.SelectedItem as String == "Копия")
                {
                    formats.IsEnabled = true;
                    TwoSides.IsEnabled = true;
                    Colors.IsEnabled = true;

                    if (formats.SelectedItem as String == "А4")
                    {
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = true;
                        LotOfColor.IsEnabled = false;
                    }
                    if (formats.SelectedItem as String == "А3")
                    {
                        TwoSides.IsEnabled = true;
                        Colors.IsEnabled = false;
                        LotOfColor.IsEnabled = false;
                    }
                    if (formats.SelectedItem as String == "А2" || formats.SelectedItem as String == "А1")
                    {
                        TwoSides.IsEnabled = false;
                        Colors.IsEnabled = true;
                        LotOfColor.IsEnabled = true;
                    }
                }
                else if (typeOperation.SelectedItem as String == "Ризограф") 
                {
                    formats.SelectedIndex = 0;
                    formats.IsEnabled = false;
                    Colors.IsEnabled = false;
                    LotOfColor.IsEnabled = false;
                }
                if (textBoxCount.Text.Length == 0)
                    textBoxCount.Text = "1";

                CostCalculations();
            }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, 0)) 
            {
                e.Handled = true;
            }
        }
    }
}
