﻿using System;
using System.Linq;
using MikeRobbins.SPEAKDemo.Interfaces;
using MikeRobbins.SPEAKDemo.Models;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace MikeRobbins.SPEAKDemo.Repositories
{
    public class NewsArticleRepository : Sitecore.Services.Core.IRepository<NewsArticle>
    {
        private INewsReader _iNewsReader;
        private INewsUpdater _iNewsUpdater;
        private INewsCreator _iNewsCreator;
        private ISitecoreUtilities _iSitecoreUtilities;

        public NewsArticleRepository(INewsReader iNewsReader, INewsUpdater iNewsUpdater, INewsCreator iNewsCreator, ISitecoreUtilities iSitecoreUtilities)
        {
            _iNewsReader = iNewsReader;
            _iNewsUpdater = iNewsUpdater;
            _iNewsCreator = iNewsCreator;
            _iSitecoreUtilities = iSitecoreUtilities;
        }

        public IQueryable<NewsArticle> GetAll()
        {
            throw new NotImplementedException();
        }

        public NewsArticle FindById(string id)
        {
            var sId = _iSitecoreUtilities.ParseId(id);

            return _iNewsReader.GetNewsArticle(sId);
        }

        public void Add(NewsArticle entity)
        {
            _iNewsCreator.ParentItem = Sitecore.Data.Database.GetDatabase("master").GetItem(new ID("{68DA2C9B-DA93-4848-9EF3-07DA58E17319}"));
            _iNewsCreator.Template = new TemplateItem(Sitecore.Data.Database.GetDatabase("master").GetItem(new ID("{18FF6308-E01B-4460-A884-C1DA8A25E515}")));

            var newItem = _iNewsCreator.CreateNewsArticle(entity);

            entity.Id = newItem.ID.ToString();
        }

        public bool Exists(NewsArticle entity)
        {
            return _iNewsReader.NewsAticleExists(entity);
        }

        public void Update(NewsArticle entity)
        {
            _iNewsUpdater.UpdateNewsArticle(entity);
        }

        public void Delete(NewsArticle entity)
        {
            _iNewsUpdater.DeleteNewsArticle(entity);
        }
    }
}