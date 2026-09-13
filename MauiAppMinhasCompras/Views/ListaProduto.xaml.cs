using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Views;
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
        try
        {


            List<Produto> tmp = await App.Db.GetAll();
            // expreção lambida para percorrer a lista e adicionar os produtos na lista
            tmp.ForEach(i => Lista.Add(i));
        }

        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }

    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
       

  

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        try 
        { 


        double soma = Lista.Sum(i => i.Total);

        string msg = $"O valor total é: {soma:C}";

        DisplayAlertAsync("Total da Lista", msg, "OK");

        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "OK");
        }


    }

    private async void txt_search_TextChanged_1(object sender, TextChangedEventArgs e)
    {

        try
        {

        
        string q  = e.NewTextValue;

        Lista.Clear();
        List<Produto> tmp = await App.Db.Search(q);
        // expreção lambida para percorrer a lista e adicionar os produtos na lista
        tmp.ForEach(i => Lista.Add(i));


        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }

    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try 
        {
            MenuItem selecinado = sender as MenuItem;
            Produto p = selecinado.BindingContext as Produto;

            bool confirm = await DisplayAlertAsync("Tem Certeza?", $"Deseja excluir o produto {p.Descricao}?", "Sim", "Não");   

                if (confirm)
            {
                await App.Db.Delete(p.Id);
                Lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
             await DisplayAlertAsync("Ops", ex.Message, "OK");
        }

    }

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        try
        {
            Produto p = e.SelectedItem as Produto;
            Navigation.PushAsync(new EditarProduto
            { BindingContext = p });
        }
        catch (Exception ex)
        {
            DisplayAlertAsync("Ops", ex.Message, "OK");
        }

    }
}

