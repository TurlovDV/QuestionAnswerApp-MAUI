using Mopups.Services;
using QuestionAnswer.ViewModel;
using QuestionAnswer.ViewModel.PopupsMainPage;

namespace QuestionAnswer.View.PopupsMainPage;

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