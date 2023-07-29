using Mopups.Services;
using QuestionAnswer.Mobile.ViewModel;
using QuestionAnswer.Mobile.ViewModel.PopupsMainPage;

namespace QuestionAnswer.Mobile.View.PopupsMainPage;

public partial class PopupCreateQuestionView
{
	public PopupCreateQuestionView()
	{
		InitializeComponent();
		
		this.BindingContext = new PopupCreateQuestionViewModel();
    }

    private void Close(object sender, EventArgs e)
    {
		MopupService.Instance.RemovePageAsync(this);
    }
}