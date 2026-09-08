using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> Lista = new ObservableCollection<Produto>();
    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = Lista;

    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        // expreção lambida para percorrer a lista e adicionar os produtos na lista
        tmp.ForEach(i => Lista.Add(i));

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
       

  

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = Lista.Sum(i => i.Total);

        string msg = $"O valor total é: {soma:C}";

        DisplayAlertAsync("Total da Lista", msg, "OK");

    }

    private async void txt_search_TextChanged_1(object sender, TextChangedEventArgs e)
    {
        string q  = e.NewTextValue;

        Lista.Clear();
        List<Produto> tmp = await App.Db.Search(q);
        // expreção lambida para percorrer a lista e adicionar os produtos na lista
        tmp.ForEach(i => Lista.Add(i));

    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {

    }
}

