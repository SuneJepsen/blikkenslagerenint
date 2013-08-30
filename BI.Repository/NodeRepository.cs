using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using umbraco;
using umbraco.NodeFactory;
using umbraco.interfaces;
using Examine;
using Examine.SearchCriteria;
using Examine.LuceneEngine.SearchCriteria;
using BI.Repository.Entities;
using System;

namespace BI.Repository
{
    public class NodeRepository
    {
        public static Node GetWebsiteRoot(Node node)
        {
            var ancestors = node.GetAncestorOrSelfNodes();
            var root = ancestors.FirstOrDefault(n => n.Level == 1);
            return root; 
        }
        public static Node GetWebsiteRootMobile(Node node)
        {
            var ancestors = node.GetAncestorOrSelfNodes();
            var root = ancestors.FirstOrDefault(n => n.Level == 2);
            return root;
        }
        public static Node GetBitorvetNewsRootNode()
        {
            return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage/BiltorvetNews");
        }
        public static Node GetCategoryFolderRootNode()
        {
          //  return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self:://CategoryFolder");
            return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage/Global/CategoryFolder");
        }
        public static Node GetAutodesktopNewsRootNode()
        {
            return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage/AutodesktopNews");
        }
        public static Node GetAccessoriesRootNode()
        {
            return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage/Accessories");
        }


        public static Node GetGlobalRootNode()
        {
            return Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::root/Global");
        }
        public static Node GetWebsiteFrontpage()
        {
            return (Node)Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage");
        }
        public static Node GetWebsiteFrontpageMobile()
        {
            return (Node)Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::MFrontpage");
        }
        public static FrontpageItem GetWebsiteFrontpageItem()
        {
            return new FrontpageItem(GetWebsiteFrontpageNode());
        }

        public static Node GetWebsiteContactpage(int nodeId)
        {
            return (Node)Node.GetNodeByXpath("//*[@id=" + nodeId + "]/ancestor-or-self::Frontpage/Contact");
        }
        public static Node GetWebsiteFrontpageNode()
        {
            return (Node)Node.GetNodeByXpath("//*[@id=" + Node.GetCurrent().Id + "]/ancestor-or-self::Frontpage");
        }
        public static List<NavigationItem> GetTopMenu()
        {
            return GetTopMenu(Node.GetCurrent());
        }

        public static List<NavigationItem> GetTopMenu(Node currentNode)
        {
            Node root = GetWebsiteRoot(currentNode);
            List<NavigationItem> result = GetNavigationItems(root, currentNode.Id).ToList();
            result[0].Class += " first";
            result[result.Count - 1].Class += " last";
            return result;
        }
        public static List<NavigationItem> GetTopMenuMobile(Node currentNode)
        {
            Node root = GetWebsiteRootMobile(currentNode);
            List<NavigationItem> result = GetNavigationItems(root, currentNode.Id).ToList();
            result[0].Class += " first";
            result[result.Count - 1].Class += " last";
            return result;
        }
        public static List<NavigationItem> GetTopMenuMobile()
        {
            return GetTopMenuMobile(Node.GetCurrent());
        }
        public static IEnumerable<NavigationItem> GetSubMenu(int nodeId)
        {
            return GetSubMenu(new Node(nodeId));
        }

        public static IEnumerable<NavigationItem> GetSubMenu(Node node)
        {
            return GetSubMenu(node, Node.GetCurrent());
        }

        public static List<NavigationItem> GetSubMenu(Node node, Node currentNode)
        {
            List<NavigationItem> result = GetNavigationItems(node, currentNode.Id).ToList();
            if (result.Any())
            {
                result[0].IsFirst = true;
                result[result.Count - 1].IsLast = true;
            }
            return result;
        }

        public static List<NavigationItem> GetBreadcrumb()
        {


            Node currentNode = uQuery.GetCurrentNode();
            var path = currentNode.Path.Substring(2);
            var nodes = uQuery.GetNodesByCsv(path)
                .Select(n => new NavigationItem(n, currentNode.Id))
                .ToList();
            if (!nodes.Any())
                return nodes;
            nodes[0].IsFirst = true;
            nodes.Last().IsLast = true;
            return nodes;
        }

        private static IEnumerable<NavigationItem> GetNavigationItems(Node root, int currentId)
        {
            IEnumerable<NavigationItem> result = root.ChildrenAsList
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true)
                .Select(n => new NavigationItem(n, currentId))
                .ToList();
            return result;
        }
        public static List<NavigationItem> GetBiltorvetSubmenu()
        {

            Node newsListRoot = GetBitorvetNewsRootNode();
            if (newsListRoot == null || !newsListRoot.ChildrenAsList.Any())
                return new List<NavigationItem>();
            List<NavigationItem> allNews = newsListRoot.GetDescendantNodesByType("Textpage")
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true && ((Node)n).GetProperty<bool>("ownSinglePage") != false)
                .Select(n => new NavigationItem(n, Node.GetCurrent().Id)).OrderByDescending(n => n.CreatedDate).ToList();
            return allNews;
        }
        public static List<NavigationItem> GetAutodesktopSubmenu()
        {

            Node newsListRoot = GetAutodesktopNewsRootNode();
            if (newsListRoot == null || !newsListRoot.ChildrenAsList.Any())
                return new List<NavigationItem>();
            List<NavigationItem> allNews = newsListRoot.GetDescendantNodesByType("Textpage")
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true && ((Node)n).GetProperty<bool>("ownSinglePage") != false)
                .Select(n => new NavigationItem(n, Node.GetCurrent().Id)).OrderByDescending(n => n.CreatedDate).ToList();
            return allNews;
        }
        public static List<StandardValueItem> GetShareholderList()
        {
            INode node = Node.GetCurrent().Parent;
            if (node == null || !node.ChildrenAsList.Any())
                return new List<StandardValueItem>();
            List<StandardValueItem> allEmployeeFolders = node.GetDescendantNodesByType("Shareholderpage")
                .Select(n => new StandardValueItem(n)).OrderByDescending(n => n.CreatedDate).ToList();

            return allEmployeeFolders;
        }
        public static UmbracoEnum.DocumentTypeAlias GetDocType()
        {
            INode currentNode =Node.GetCurrent();

            while (currentNode.NodeTypeAlias == "Textpage")
            {
                currentNode =currentNode.Parent;
            }
            return UmbracoEnum.GetDocType(currentNode);
        }

        public static List<SliderItem> GetFrontpageSlider(string idCsv)
        {

            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<SliderItem>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new SliderItem(n)).ToList();
        }
        public static List<Testimonial> GetFrontpageTestimonials(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<Testimonial>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new Testimonial(n)).ToList();;
        }

        public static List<TeaserItem> GetTeasers(string idCsv)
        {

            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<TeaserItem>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new TeaserItem(n)).ToList();
        }

        public static List<PortfolioItem> GetPortfolioList()
        {

            Node portfolioRoot = Node.GetCurrent();
            if (portfolioRoot == null || !portfolioRoot.ChildrenAsList.Any())
                return new List<PortfolioItem>();
            List<PortfolioItem> allPortfolioItems = portfolioRoot.GetDescendantNodesByType("PortfolioPage")
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true)
                .Select(n => new PortfolioItem(n)).OrderByDescending(n => n.CreatedDate).ToList();
            return allPortfolioItems;
        }



        public static List<EmployeeFolder> GetEmployeeFolders(INode node)

        {
            if (node == null || !node.ChildrenAsList.Any())
                return new List<EmployeeFolder>();
            List<EmployeeFolder> allEmployeeFolders = node.GetDescendantNodesByType("EmployeeFolder")
                .Select(n => new EmployeeFolder(n)).ToList();

            return allEmployeeFolders;
        }
        public static IEnumerable<Employee> GetEmployeeItems(INode node)
        {
            if (node == null || !node.ChildrenAsList.Any())
                return new List<Employee>();
            IEnumerable<Employee> allEmployees = node.GetDescendantNodesByType("Employee")
                                                     .Select(n => new Employee(n));

            return allEmployees;
        }
        public static IEnumerable<Testimonial> GetTestimonialList(INode node)
        {
            if (node == null || !node.ChildrenAsList.Any())
                return new List<Testimonial>();
            IEnumerable<Testimonial> allTestimonials = node.GetDescendantNodesByType("Testimonial")
                .Select(n => new Testimonial(n)).ToList();

            return allTestimonials;
        }
        public static IEnumerable<Reference> GetReferenceList(INode node)
        {
            if (node == null || !node.ChildrenAsList.Any())
                return new List<Reference>();
            IEnumerable<Reference> allReferences = node.GetDescendantNodesByType("Reference")
                .Select(n => new Reference(n)).ToList();

            return allReferences;
        }

        public static TileItem GetSingleTile(string nodeId)
        {
            return (new TileItem(int.Parse(nodeId)));
        }
        public static Employee GetEmployee(string nodeId)
        {
            return (new Employee(int.Parse(nodeId)));
        }
        public static List<Employee> GetEmployeeTileList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<Employee>();
            }
            else
            {
                List<Employee> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new Employee(n)).ToList();
                return tileList;
            }
        }
        public static List<EmployeeFolder> GetEmployeeFolder(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<EmployeeFolder>();
            }
            else
            {
                List<EmployeeFolder> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new EmployeeFolder(n)).ToList();
                return tileList;
            }
        }
        public static List<Employee> GetEmployeeList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<Employee>();
            }
            else
            {
                List<Employee> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new Employee(n)).ToList();
                return tileList;
            }
        }
        public static List<ContentItem> GetContentItems(INode node)
        {
            if (node == null || !node.ChildrenAsList.Any())
                return new List<ContentItem>();
            List<ContentItem> allEmployees = node.ChildrenAsList
                .Select(n => new ContentItem(n)).ToList();

            return allEmployees;
        }
        public static List<TileItem> GetTileList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<TileItem>();
            }
            else
            {
                List<TileItem> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new TileItem(n)).ToList();
                return tileList;
            }
        }

        public static List<TileItem> GetTilelistByNodeTypeAlias(string nodeTypeAlias)
        {
            Node currentNode = Node.GetCurrent();
            List<TileItem> result = currentNode.ChildrenAsList
                .Where(n => ((Node)n).NodeTypeAlias == nodeTypeAlias)
                .Select(n => new TileItem(n))
                .ToList();
            return result;
        }
        public static List<TileItem> GetTilelist()
        {
            Node currentNode = Node.GetCurrent();
            List<TileItem> result = currentNode.ChildrenAsList
                .Where(n => ((Node)n).GetProperty<bool>("tileHide") != true)
                .Select(n => new TileItem(n))
                .ToList();
            return result;
        }

        public static List<Car> GetCarTileList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<Car>();
            }
            else
            {
                List<Car> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new Car(n)).ToList();
                return tileList;
            }
        }
        public static List<GarageAccessories> GetGarageCarTileList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<GarageAccessories>();
            }
            else
            {
                List<GarageAccessories> tileList = uQuery.GetNodesByCsv(idCsv).Select(n => new GarageAccessories(n)).ToList();
                return tileList;
            }
        }



        public static List<List<SearchItem>> Split(List<SearchItem> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static List<List<GarageAccessories>> Split(List<GarageAccessories> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static List<List<TileItem>> Split(List<TileItem> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static List<List<Car>> Split(List<Car> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
        public static List<List<Employee>> Split(List<Employee> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 4)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }
     


        public static Link GetLink(string linksXml)
        {
          
                XDocument linksDoc = XDocument.Parse(linksXml);
                return new Link(linksDoc.Element("url-picker"));
        
            
        }
        public static Link GetLink(Node node)
        {
            return new Link(node);
        }
        public static Link GetLink(string linksXml, Node node)
        {
            XDocument linksDoc = XDocument.Parse(linksXml);
            return new Link(linksDoc.Element("url-picker"), node);
        }

        public static List<StandardValueItem> GetAccessorieslists()
        {
            Node currentNode = Node.GetCurrent();
            List<StandardValueItem> result = currentNode.ChildrenAsList
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true)
                .Select(n => new StandardValueItem(n)).OrderByDescending(n => n.CreatedDate)
                .ToList();
            return result;
        }
        public static List<NewsItem> GetNewslists()
        {
            Node currentNode = Node.GetCurrent();
            List<NewsItem> result = currentNode.ChildrenAsList
                .Where(n => ((Node)n).GetProperty<bool>("umbracoNaviHide") != true)
                .Select(n => new NewsItem(n))
                .ToList();
            return result;
        }
        public static List<StandardValueItem> GetNewslists(string pageAlias, int numberOfNews = 4)
        {
            Node currentNode = Node.GetCurrent();
            Node frontpage = GetWebsiteRoot(currentNode);
            List<StandardValueItem> newsItems = new List<StandardValueItem>();
            Node nodeNewsRoot = Node.GetNodeByXpath("//*[@id=" + frontpage.Id + "]//" + pageAlias);
            newsItems = nodeNewsRoot.GetDescendantNodesByType("Textpage").Take(numberOfNews).Select(n => new StandardValueItem(n)).OrderByDescending(n => n.CreatedDate).ToList();
            return newsItems;


        }
        public static TileItem GetRandomNewsItem()
        {
            Node currentNode = Node.GetCurrent();
            Node frontpage = NodeRepository.GetWebsiteRoot(currentNode);
            TileItem tileItem = new TileItem();

            Node nodeNewsRoot = Node.GetNodeByXpath("//*[@id=" + frontpage.Id + "]//News");
            if (nodeNewsRoot == null)
                return tileItem;


            List<Node> nodeNewsItems = nodeNewsRoot.GetDescendantNodesByType("Textpage").ToList();

            if (nodeNewsItems != null && nodeNewsItems.Any())
            {
                Random rnd = new Random();
                int newsItemIndex = rnd.Next(0, nodeNewsItems.Count());
                Node newsItem = nodeNewsItems.ToList()[newsItemIndex];
                tileItem = new TileItem((INode)newsItem);

            }
            return tileItem;


        }

        public static List<StandardValueItem> GetProductlists()
        {
            Node currentNode = Node.GetCurrent();
            Node frontpage = GetWebsiteRoot(currentNode);
            List<StandardValueItem> productItems = new List<StandardValueItem>();
            productItems = frontpage.GetDescendantNodesByType("Product").Select(n => new StandardValueItem(n)).ToList();
            return productItems;


        }

        public static List<TileItem> GetLatestAccessoriesItems(int count = 0, int skip = 0)
        {
            Node accessoriesListRoot = GetAccessoriesRootNode();
            if (accessoriesListRoot == null || !accessoriesListRoot.ChildrenAsList.Any())
                return new List<TileItem>();
            List<TileItem> allAccessories = accessoriesListRoot.GetDescendantNodesByType("Textpage")
                .Select(n => new TileItem(n)).ToList();
            if (count != 0)
                return allAccessories.OrderByDescending(n => n.PublishDate).Skip(skip).Take(count).ToList();
            return allAccessories.OrderByDescending(n => n.PublishDate).ToList();
        }
        public static List<NewsItem> GetLatestBiltorvetNewsItems(int count = 0, int skip = 0)
        {

            Node newsListRoot = GetBitorvetNewsRootNode();
            if (newsListRoot == null || !newsListRoot.ChildrenAsList.Any())
                return new List<NewsItem>();
            List<NewsItem> allNews = newsListRoot.GetDescendantNodesByType("Textpage")
                .Select(n => new NewsItem(n)).ToList();
            if (count != 0)
                return allNews.OrderByDescending(n => n.PublishDate).Skip(skip).Take(count).ToList();
            return allNews.OrderByDescending(n => n.PublishDate).ToList();
        }
        public static List<NewsItem> GetLatestAutodesktopNewsItems(int count = 0, int skip = 0)
        {

            Node newsListRoot = GetAutodesktopNewsRootNode();
            if (newsListRoot == null || !newsListRoot.ChildrenAsList.Any())
                return new List<NewsItem>();
            List<NewsItem> allNews = newsListRoot.GetDescendantNodesByType("Textpage")
                .Select(n => new NewsItem(n)).ToList();
            if (count != 0)
                return allNews.OrderByDescending(n => n.PublishDate).Skip(skip).Take(count).ToList();
            return allNews.OrderByDescending(n => n.PublishDate).ToList();
        }
        // public static List<NewsItem> GetNewsItems(int id, int count = 0, int skip = 0)
        // {
        //     var rootNode = new Node(id);
        //     IOrderedEnumerable<NewsItem> allNewsItems = rootNode.ChildrenAsList
        //         .Select(n => new NewsItem(n))
        //         .OrderByDescending(ni => ni.PublishDate);
        //     if (count != 0)
        //         return allNewsItems.Skip(skip).Take(count).ToList();
        //     return allNewsItems.ToList();
        // }
        // public static List<NewsItem> GetNewsItems(string idCsv)
        // {
        //     if (string.IsNullOrWhiteSpace(idCsv))
        //         return new List<NewsItem>();
        //     return uQuery.GetNodesByCsv(idCsv).Select(n => new NewsItem((INode)n)).ToList();
        // }
        // public static List<TeaserItem> GetEventTeasers(int id)
        // {
        //     var rootNode = new Node(id);
        //     return uQuery.GetNodesByCsv(rootNode.GetProperty<string>("topTeaserSlider"))
        //         .Select(n => new TeaserItem(n))
        //         .ToList();
        // }

        // public static List<GalleryItem> GetGalleryItemList(string idCsv)
        // {
        //     if (string.IsNullOrWhiteSpace(idCsv))
        //         return new List<GalleryItem>();
        //     return uQuery.GetMediaByCsv(idCsv).Select(n => new GalleryItem(n)).ToList();
        // }
        // public static List<BlogItem> GetLatestBlogItems(int take)
        // {
        //     var rootNode = GetWebsiteFrontpage();
        //     IOrderedEnumerable<BlogItem> allBlogItems = rootNode.GetDescendantNodesByType("Blogpost")
        //         .Select(n => new BlogItem((INode)n))
        //         .OrderByDescending(ni => ni.PublishDate);
        //     return allBlogItems.Take(take).ToList();

        // }
        // public static List<BlogItem> GetBlogPostList(string idCsv)
        // {
        //     if (string.IsNullOrWhiteSpace(idCsv))
        //         return new List<BlogItem>();
        //     return uQuery.GetNodesByCsv(idCsv).Select(n => new BlogItem((INode)n)).ToList();
        // }
        // public static List<BlogItem> GetBlogPostList(int id, int size,  int year, string category)
        // {

        //     var rootNode = new Node(id);
        //     IQueryable<BlogItem> allBlogItems = rootNode.GetDescendantNodesByType("Blogpost")
        //         .Select(n => new BlogItem((INode)n))
        //         .OrderByDescending(ni => ni.PublishDate).AsQueryable();

        //     if (year > 0)
        //     {
        //         allBlogItems =  allBlogItems.Where(x => x.PublishDate.Year == year);
        //     }

        //     if (!string.IsNullOrEmpty(category))
        //     {
        //         allBlogItems =  allBlogItems.Where(x => x.Categories.Contains(category));
        //     }
        //     return allBlogItems.Take(size).ToList();
        // }

        // public static BlogSidebarItem GetBlogSidebarForBlogList(int id,  int numberBlogPosts)
        // {

        //     var rootNode = new Node(id);
        //     List<BlogItem> allBlogItems = rootNode.GetDescendantNodesByType("Blogpost")
        //         .Select(n => new BlogItem((INode)n))
        //         .OrderByDescending(ni => ni.PublishDate).ToList();

        //     return CreateBlogSidebar(allBlogItems, numberBlogPosts);
        // }
        // public static BlogSidebarItem GetBlogSidebarForBlogPost(int id)
        // {

        //     var currentNode = new Node(id);
        //     INode rootNode = currentNode;

        //     while (rootNode.NodeTypeAlias != "Blog")
        //     {
        //         rootNode = rootNode.Parent;
        //     }
        //     int numberBlogPosts = int.Parse(rootNode.GetProperty("latestBlog").Value);

        //    List<BlogItem> allBlogItems =((Node)rootNode).GetDescendantNodesByType("Blogpost")
        //         .Select(n => new BlogItem((INode)n))
        //         .OrderByDescending(ni => ni.PublishDate).ToList();

        //    return CreateBlogSidebar(allBlogItems, numberBlogPosts);

        // }

        // private static BlogSidebarItem CreateBlogSidebar(List<BlogItem> allBlogItems, int numberBlogPosts)
        // {
        //     BlogSidebarItem blogSidebarItem = new BlogSidebarItem();
        //     blogSidebarItem.Categories.AddRange(allBlogItems.SelectMany(x => x.Categories.Select(y => y.ToString())).Distinct().ToList());
        //     blogSidebarItem.Archives.AddRange(allBlogItems.Select(x => x.PublishDate.Year.ToString()).Distinct().ToList());
        //     blogSidebarItem.BlogItems = allBlogItems.Take(numberBlogPosts).ToList();
        //     return blogSidebarItem;
        // }

        // public static List<ResearchItem> GetResearchItems(int id)
        // {

        //     var rootNode = new Node(id);
        //     List<INode> items = rootNode.ChildrenAsList;
        //     List<ResearchItem> researchItems = new List<ResearchItem>();
        //     if (items.Any())
        //     {
        //         if (items[0].NodeTypeAlias == "ArticleList")
        //         {
        //             //2012
        //             researchItems = rootNode.GetDescendantNodesByType("Article")
        //                 .Select(n => new ResearchItem((INode)n))
        //                 .OrderByDescending(ni => ni.PublishDate).ToList();
        //         }
        //         else
        //         {
        //             //Artikelliste/Bibliotek - 2012
        //             researchItems = items.Select(n => new ResearchItem((INode)n))
        //                 .OrderByDescending(ni => ni.PublishDate).ToList();
        //         }
        //     }


        //     if (researchItems.Any())
        //         researchItems.Last().Class = "last";

        //     return researchItems;

        // }
        //public static List<ResearchlistItem> GetResearchlists()
        // {
        //     Node currentNode = Node.GetCurrent();
        //     Node root = GetResearchFromArticleListRoot(currentNode);
        //     List<ResearchlistItem> result = root.ChildrenAsList
        //         .Where(n => ((Node) n).GetProperty<bool>("umbracoNaviHide") != true)
        //         .Select(n => new ResearchlistItem(n))
        //         .OrderByDescending(ni => ni.NavText)
        //         .ToList();
        //     if (result.Any(r => r.Id == currentNode.Id))
        //         result.First(r => r.Id == currentNode.Id).Class = "current";

        //     return result;
        // }
        //public static List<ResearchItem> GetLatestResearchItems()
        //{
        //    Node articleListRoot = GetResearchFromArticleListRoot((INode)Node.GetCurrent().Parent);
        //    int take = articleListRoot.GetProperty<int>("latestArticles");
        //    List<ResearchItem> researchsItems = articleListRoot.GetDescendantNodesByType("Article")
        //        .Select(n => new ResearchItem(n)).OrderByDescending(n => n.PublishDate).Take(take).ToList();

        //    return researchsItems;
        //}
        //private static Node GetResearchFromArticleListRoot(INode node)
        //{

        //    while (node.NodeTypeAlias == "ArticleList")
        //    {
        //        if (node.Parent.NodeTypeAlias != "ArticleList")
        //            break;
        //        node = node.Parent;
        //    }
        //    return (Node)node;
        //}

        public static List<SearchItem> GetPages(string searchTerm)
        {
            IEnumerable<Examine.SearchResult> examineSearchResults;
            List<SearchItem> searchResults;
            var criteria = ExamineManager.Instance.SearchProviderCollection["AVSearcher"].CreateSearchCriteria(BooleanOperation.Or);
            var query = GetFilter(searchTerm, criteria);
            examineSearchResults = ExamineManager.Instance.SearchProviderCollection["AVSearcher"].Search(query.Compile());
            if (examineSearchResults.Any())
            {
                searchResults = examineSearchResults.Select(x => new SearchItem((INode)new Node(x.Id))).ToList();
                return searchResults;
            }
            return null;
        }

        private static IBooleanOperation GetFilter(string searchTerm, ISearchCriteria criteria)
        {
            IBooleanOperation result;
            // Handle searching for multiple terms 
            var textFields = new[] { "header", "headerSmall", "manchet", "description", "bodyText1", "bodyText2" };
            if (searchTerm.Contains(" "))
            {
                string[] terms = searchTerm.Split(' ');
                result = criteria.GroupedOr(textFields, new[] { LuceneSearchExtensions.MultipleCharacterWildcard(terms[0]) });
                for (int i = 1; i < terms.Length; i++)
                {
                    result.Or().GroupedOr(textFields,
                        new[] { LuceneSearchExtensions.MultipleCharacterWildcard(terms[i]) });
                }

            }
            else
            {
                result = criteria.GroupedOr(new string[] { "header", "headerSmall", "manchet", "description", "bodyText1", "bodyText2" }, searchTerm.ToLower()).Or().
                    GroupedOr(new string[] { "header", "headerSmall", "manchet", "description", "bodyText1", "bodyText2" }, searchTerm.ToUpper()).Or().
                    GroupedOr(new string[] { "header", "headerSmall", "manchet", "description", "bodyText1", "bodyText2" }, searchTerm);
            }
            return result;
        }

        //public static FooterItem GetFooter()
        //{

        //    return new FooterItem(GetWebsiteFrontpage());
        //}
        //GetWebsiteFrontpage


        public static List<SliderItem> PageSlider(string idCsv)
        {

            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<SliderItem>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new SliderItem(n)).ToList();
        }

        public static GalleryItem GetGallery(Node node)
        {
            return new GalleryItem(node);
        }
        public static List<Category> GetCategoryList(string idCsv)
        {

            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<Category>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new Category(n)).ToList();
        }

        public static List<Category> GetCategoryList()
        {

            Node categoryListRoot = GetCategoryFolderRootNode();
            if (categoryListRoot == null || !categoryListRoot.ChildrenAsList.Any())
                return new List<Category>();
            List<Category> allNews = categoryListRoot.GetDescendantNodesByType("Category")
                .Select(n => new Category(n)).OrderByDescending(n => n.CreatedDate).ToList();
            return allNews;
        }
        public static List<AreaItem> GetAreas(Node currentNode)
        {

            List<AreaItem> allAreas = currentNode.GetDescendantNodesByType("Area")
                .Select(n => new AreaItem(n)).ToList();
            return allAreas;
        }
        public static List<DepartmentItem> GetDepartments(string idCsv)
        {

            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<DepartmentItem>();
            return uQuery.GetNodesByCsv(idCsv).Select(n => new DepartmentItem(n)).ToList();
        }
        public static List<DepartmentItem> GetDepartment(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
                return new List<DepartmentItem>();
            List<DepartmentItem> departments = GetDepartments(idCsv);
            if (departments != null && departments.Any())
                return departments;
            return new List<DepartmentItem>();
        }
        public static List<DepartmentItem> GetAllDepartments()
        {
            Node globalRoot = GetGlobalRootNode();

            List<DepartmentItem> departments = globalRoot.GetDescendantNodesByType("Department").Select(n => new DepartmentItem(n)).ToList();

 
            return departments;
        }
        public static List<ContactSubject> GetContactSubjects(Node currentNode)
        {
            string idCsv = currentNode.GetProperty<string>("contactSubjects");
            if (!string.IsNullOrWhiteSpace(idCsv))
            {
                return uQuery.GetNodesByCsv(idCsv).Select(n => new ContactSubject(n)).ToList();

            }
            return new List<ContactSubject>();

        }
        public static List<CarBrandItem> GetCarBrandList(string idCsv)
        {
            if (string.IsNullOrWhiteSpace(idCsv))
            {
                return new List<CarBrandItem>();
            }
            else
            {
                List<CarBrandItem> carBrandList = uQuery.GetNodesByCsv(idCsv).Select(n => new CarBrandItem(n)).ToList();
                return carBrandList;
            }
        }
        public static List<List<StandardValueItem>> Split(List<StandardValueItem> source)
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 2)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }


     
    }
}