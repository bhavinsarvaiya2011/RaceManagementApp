using AutoMapper;
using RaceManagementApp.UI.Utility.Logger;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RaceManagementApp.UI.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        private readonly IMapper _mapper;

        public event PropertyChangedEventHandler PropChanged;

        public ViewModelBase(IMapper mapper) 
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void MapObject<TSource, TTarget>(TSource source, TTarget target, string propertyName)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            target = default;
            target = _mapper.Map<TTarget>(source);

            OnPropertyChanged(propertyName);
        }

        protected void MapList<TSource, TTarget>(IEnumerable<TSource> sourceList, ObservableCollection<TTarget> targetList, string propertyName)
        {
            if (sourceList == null)
            {
                targetList.Clear();
            }
            else
            {
                targetList.Clear();
                foreach (var item in _mapper.Map<IEnumerable<TTarget>>(sourceList))
                {
                    targetList.Add(item);
                }
            }

            OnPropertyChanged(propertyName);
        }

    }
}
