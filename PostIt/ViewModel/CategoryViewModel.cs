using PostIt.Database;
using PostIt.Model;
using PostIt.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using ChangedAction = System.Collections.Specialized.NotifyCollectionChangedAction;
namespace PostIt.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel(Category category)
        {
            Category = category;

            foreach (Model.PostIt postIt in category.PostIts)
            {
                PostIts.Insert(0, new PostItViewModel(postIt));
            }
            category.PostIts.CollectionChanged += PostItsCollectionChanged;
        }

        private void PostItsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch(e.Action)
            {
                case ChangedAction.Add:
                    foreach (var newItem in e.NewItems)
                    {
                        PostIts.Insert(0, new PostItViewModel(newItem as Model.PostIt));
                    }
                    break;
                case ChangedAction.Remove:
                    foreach (var oldItem in e.OldItems)
                    {
                        PostIts.Remove(PostIts.FirstOrDefault(p => (p as PostItViewModel).Model.Id == (oldItem as Model.PostIt).Id));
                    }
                    break;
                case ChangedAction.Reset:
                    PostIts.Clear();
                    break;
            }
            OnPropertyChanged();
        }

        internal void Drop(PostItViewModel postItViewModel)
        {
            PostItContext.ChangePostItCategory(postItViewModel.Model, Category);
        }

        public Category Category { get; protected set; }

        public ObservableCollection<BaseViewModel> PostIts { get; protected set; } = new ObservableCollection<BaseViewModel>();

    }
}
