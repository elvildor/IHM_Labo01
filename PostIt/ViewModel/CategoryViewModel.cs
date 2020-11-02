using PostIt.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostIt.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel(Category category)
        {
            Category = category;

            foreach (Model.PostIt postIt in category.PostIts)
            {
                PostIts.Add(new PostItViewModel(postIt));
            }
        }

        public Category Category { get; protected set; }

        public List<BaseViewModel> PostIts { get; protected set; } = new List<BaseViewModel>();

    }
}
