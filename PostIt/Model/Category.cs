using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PostIt.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ObservableCollection<PostIt> PostIts { get; set; }

    }
}
