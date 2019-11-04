using DMData.Names;

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

namespace Name_and_NPC_Generator
{
    public sealed partial class MainPage : Page
    {
        enum MilitaryGradeSelection
        {
            Enlisted,
            Officer,
            Combined,
        }
        enum NobilityGenderSelection
        { 
            Female,
            Male,
        }

        public MainPage()
        {
            this.InitializeComponent();

            this.nameCategories.SelectionChanged += NameCategories_SelectionChanged;
            this.titleCategories.SelectionChanged += TitleCategories_SelectionChanged;
            this.militaryBranches.SelectionChanged += MilitaryBranches_SelectionChanged;
            this.militaryCategories.SelectionChanged += MilitaryCategories_SelectionChanged;
            this.militaryGrades.SelectionChanged += MilitaryGrades_SelectionChanged;
            this.nobilityGenders.SelectionChanged += NobilityGenders_SelectionChanged;
            this.nobilityCategories.SelectionChanged += NobilityCategories_SelectionChanged;

            this.viewDescriptors.SelectionChanged += ViewDescriptors_SelectionChanged;
            this.viewFemaleNames.SelectionChanged += ViewFemaleNames_SelectionChanged;
            this.viewMaleNames.SelectionChanged += ViewMaleNames_SelectionChanged;
            this.viewSurnames.SelectionChanged += ViewSurnames_SelectionChanged;
            this.viewTitlesGeneral.SelectionChanged += ViewTitlesGeneral_SelectionChanged;
            this.viewTitlesMilitary.SelectionChanged += ViewTitlesMilitary_SelectionChanged;
            this.viewTitlesNobility.SelectionChanged += ViewTitlesNobility_SelectionChanged;

            this.clearSelectedDescriptor.Click += ClearSelectedDescriptor_Click;
            this.clearSelectedGivenName.Click += ClearSelectedGivenName_Click;
            this.clearSelectedSurname.Click += ClearSelectedSurname_Click;
            this.clearSelectedTitle.Click += ClearSelectedTitle_Click;

            this.getRandomFemale.Click += GetRandomFemale_Click;
            this.getRandomMale.Click += GetRandomMale_Click;
            this.getRandomName.Click += GetRandomName_Click;
            
            this.viewDescriptors.ItemsSource = NamesManagement.DescriptorData;
            this.SetNameComboBoxes();
            this.GetRandomName();
        }

        private void GetRandomName_Click(object sender, RoutedEventArgs e) => this.GetRandomName();
        private void GetRandomMale_Click(object sender, RoutedEventArgs e) => this.GetRandomMale();
        private void GetRandomFemale_Click(object sender, RoutedEventArgs e) => this.GetRandomFemale();

        private void ClearSelectedTitle_Click(object sender, RoutedEventArgs e) 
        { 
            this.selectedTitle.Text = "";
            this.selectedTitleBar.Text = "";
        }
        private void ClearSelectedSurname_Click(object sender, RoutedEventArgs e) 
        { 
            this.selectedSurname.Text = "";
            this.selectedSurnameBar.Text = "";
        }
        private void ClearSelectedGivenName_Click(object sender, RoutedEventArgs e) 
        { 
            this.selectedGivenName.Text = "";
            this.selectedCategory.Text = "";
            this.selectedGender.Text = "";
            this.selectedGivenNameBar.Text = "";
            this.selectedCategoryBar.Text = "";
            this.selectedGenderBar.Text = "";
        }
        private void ClearSelectedDescriptor_Click(object sender, RoutedEventArgs e) 
        { 
            this.selectedDescriptor.Text = "";
            this.selectedDescriptorBar.Text = "";
        }

        private void ViewTitlesNobility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewTitlesNobility.SelectedIndex > -1)
            {
                this.selectedTitle.Text = ((Nobility)this.viewTitlesNobility.SelectedItem).Title;
                this.selectedTitleBar.Text = this.selectedTitle.Text;
            }
        }
        private void ViewTitlesMilitary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewTitlesMilitary.SelectedIndex > -1)
            {
                this.selectedTitle.Text = ((MilitaryRank)this.viewTitlesMilitary.SelectedItem).Rank;
                this.selectedTitleBar.Text = this.selectedTitle.Text;
            }
        }
        private void ViewTitlesGeneral_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewTitlesGeneral.SelectedIndex > -1)
            {
                this.selectedTitle.Text = ((Title)this.viewTitlesGeneral.SelectedItem).Name;
                this.selectedTitleBar.Text = this.selectedTitle.Text;
            }
        }
        private void ViewSurnames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewSurnames.SelectedIndex > -1)
            {
                this.selectedSurname.Text = this.viewSurnames.SelectedItem.ToString();
                this.selectedSurnameBar.Text = this.selectedSurname.Text;
            }
        }
        private void ViewMaleNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewMaleNames.SelectedIndex > -1)
            {
                this.selectedGivenName.Text = this.viewMaleNames.SelectedItem.ToString();
                this.selectedGender.Text = NameGenderType.Male.ToString();
                this.selectedCategory.Text = this.nameCategories.SelectedItem.ToString();
                
                this.selectedGivenNameBar.Text = this.selectedGivenName.Text;
                this.selectedGenderBar.Text = this.selectedGender.Text;
                this.selectedCategoryBar.Text = this.selectedCategory.Text;
            }
        }
        private void ViewFemaleNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewFemaleNames.SelectedIndex > -1)
            {
                this.selectedGivenName.Text = this.viewFemaleNames.SelectedItem.ToString();
                this.selectedGender.Text = NameGenderType.Female.ToString();
                this.selectedCategory.Text = this.nameCategories.SelectedItem.ToString();

                this.selectedGivenNameBar.Text = this.selectedGivenName.Text;
                this.selectedGenderBar.Text = this.selectedGender.Text;
                this.selectedCategoryBar.Text = this.selectedCategory.Text;
            }
        }
        private void ViewDescriptors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.viewDescriptors.SelectedIndex > -1)
            {
                this.selectedDescriptor.Text = this.viewDescriptors.SelectedItem.ToString();
                this.selectedDescriptorBar.Text = this.selectedDescriptor.Text;
            }
        }


        private void NobilityCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetNobilityView();
        private void NobilityGenders_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetNobilityView();
        private void MilitaryGrades_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetMilitaryView();
        private void MilitaryCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetMilitaryBranches();
        private void MilitaryBranches_SelectionChanged(object sender, SelectionChangedEventArgs e) => SetMilitaryView();
        private void TitleCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.SetTitleView();
        private void NameCategories_SelectionChanged(object sender, SelectionChangedEventArgs e) => this.SetNameViews();


        void SetNobilityView()
        {
            if (this.nobilityCategories.SelectedIndex > -1 && this.nobilityGenders.SelectedIndex > -1)
            {
                var output = NamesManagement.NobilityData
                    .Where(a => a.Category == (NobilityCategoryType)this.nobilityCategories.SelectedItem);
                if ((NobilityGenderSelection)this.nobilityGenders.SelectedItem == NobilityGenderSelection.Female)
                {
                    viewTitlesNobility.ItemsSource = output.Where(a => a.Gender == NameGenderType.Female);
                }
                else { viewTitlesNobility.ItemsSource = output.Where(a => a.Gender == NameGenderType.Male); }
            }
        }
        void SetMilitaryBranches()
        {
            if (this.militaryCategories.SelectedIndex > -1)
            {
                this.militaryBranches.ItemsSource = NamesManagement.MilitaryData
                    .Where(a => a.Category == (MilitaryCategoryType)this.militaryCategories.SelectedItem)
                    .Select(b => b.Branch)
                    .Distinct()
                    .ToList();

                this.militaryBranches.SelectedIndex = 0;
            }
        }
        void SetMilitaryView()
        {
            if (this.militaryCategories.SelectedIndex > -1 && this.militaryBranches.SelectedIndex > -1 && this.militaryGrades.SelectedIndex > -1)
            {
                var output = NamesManagement.MilitaryData
                    .Where(a => a.Category == (MilitaryCategoryType)this.militaryCategories.SelectedItem)
                    .Where(b => b.Branch == (MilitaryBranchType)this.militaryBranches.SelectedItem).First();

                switch ((MilitaryGradeSelection)this.militaryGrades.SelectedItem)
                {
                    case MilitaryGradeSelection.Enlisted:
                        this.viewTitlesMilitary.ItemsSource = output.EnlistedRanks;
                        break;
                    case MilitaryGradeSelection.Officer:
                        this.viewTitlesMilitary.ItemsSource = output.OfficerRanks;
                        break;
                    case MilitaryGradeSelection.Combined:
                        this.viewTitlesMilitary.ItemsSource = output.RanksCombined;
                        break;

                }
            }
        }
        void SetTitleView()
        {
            if (this.titleCategories.SelectedIndex > -1)
            {
                this.viewTitlesGeneral.ItemsSource = NamesManagement.TitleData
                    .Where(a => a.Category == (TitleCategoryType)this.titleCategories.SelectedItem)
                    .Select(b => b.Titles).First();
            }
        }
        void SetNameViews()
        {
            if (this.nameCategories.SelectedIndex > -1)
            {
                var output = (CategoryNames)NamesManagement.NameData
                    .Where(a => a.Category == (NameCategoryType)this.nameCategories.SelectedItem)
                    .FirstOrDefault();
                this.viewFemaleNames.ItemsSource = output.FemaleNames;
                this.viewMaleNames.ItemsSource = output.MaleNames;
                if (output.HasSurnames) { this.viewSurnames.ItemsSource = output.Surnames; }
            }
        }
        void SetNameComboBoxes()
        {
            this.nameCategories.ItemsSource = NamesManagement.NameCategories;
            this.nameCategories.SelectedIndex = 0;

            this.nobilityCategories.ItemsSource = NamesManagement.NobilityCategories;
            this.nobilityCategories.SelectedIndex = 0;

            this.nobilityGenders.ItemsSource = Enum.GetValues(typeof(NobilityGenderSelection))
                .Cast<NobilityGenderSelection>().ToList();
            this.nobilityGenders.SelectedIndex = 0;

            this.militaryCategories.ItemsSource = NamesManagement.MilitaryCategories;
            this.militaryCategories.SelectedIndex = 0;

            this.militaryGrades.ItemsSource = Enum.GetValues(typeof(MilitaryGradeSelection))
                .Cast<MilitaryGradeSelection>().ToList();
            this.militaryGrades.SelectedIndex = 0;

            this.titleCategories.ItemsSource = NamesManagement.TitleCategories;
            this.titleCategories.SelectedIndex = 0;
        }

        void DisplayRandomName(RandomName name)
        {
            this.selectedCategory.Text = name.Category.ToString();
            this.selectedGender.Text = name.Gender.ToString();
            this.selectedGivenName.Text = name.GivenName;
            this.selectedSurname.Text = name.Surname;

            this.selectedCategoryBar.Text = name.Category.ToString();
            this.selectedGenderBar.Text = name.Gender.ToString();
            this.selectedGivenNameBar.Text = name.GivenName;
            this.selectedSurnameBar.Text = name.Surname;
        }
        void GetRandomName()
        {
            this.DisplayRandomName(NamesManagement.GetRandomName());
        }
        void GetRandomFemale()
        {
            if (this.nameCategories.SelectedIndex > -1)
            {
                this.DisplayRandomName(NamesManagement.GetRandomName((NameCategoryType)this.nameCategories.SelectedItem, NameGenderType.Female));
            }
        }
        void GetRandomMale()
        {
            if (this.nameCategories.SelectedIndex > -1)
            {
                this.DisplayRandomName(NamesManagement.GetRandomName((NameCategoryType)this.nameCategories.SelectedItem, NameGenderType.Male));
            }
        }
    }
}
