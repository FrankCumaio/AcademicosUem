using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AcademicosUem.Models;
using AcademicosUem.ViewModels;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Drive.v2.Data;
using System.Diagnostics;

namespace AcademicosUem.Controllers
{
    public class TrabalhoController : Controller
    {
        private AcademicosUemDbContext db = new AcademicosUemDbContext();
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Drive API Quickstart";

        public void UploadFile()
        {
            UserCredential credential;
            var path = Server.MapPath(@"~/Scripts/client_secret.json");
            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-Demo");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });


            Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File();
            body.Title = "test upload";
            body.Description = "test upload";
            body.MimeType = "application/pdf";
            var parentId = "0BwiuFVfOZIECU0w1dnA0WlR6UkU";
            if (!String.IsNullOrEmpty(parentId))
            {
                body.Parents = new List<ParentReference>()
          {new ParentReference() {Id = parentId}};
            }
            // File's content.
            byte[] byteArray = System.IO.File.ReadAllBytes("/GuiaPagamentoReport.pdf");
            MemoryStream streamf = new MemoryStream(byteArray);
            try
            {
                FilesResource.InsertMediaUpload request = service.Files.Insert(body, streamf, "application/pdf");
                request.Upload();

                Google.Apis.Drive.v2.Data.File file = request.ResponseBody;

                // Uncomment the following line to print the File ID.
               Debug.WriteLine("File ID: " + file.Id);

            }
            catch (Exception e)
            {
                Debug.WriteLine("An error occurred: " + e.Message);
            }

        }

        // GET: Trabalho
        public ActionResult Index()
        {
            var trabalho = db.Trabalho.Include(t => t.Area);
            return View(trabalho.ToList());
        }

        public ActionResult Todos()
        {
            var trabalho = db.Trabalho.Include(t => t.Area);
            return View(trabalho.ToList());
        }

        // GET: Trabalho/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            ViewBag.trabalhosRelacionados = db.Trabalho.ToList();

            if (trabalho == null)
            {
                return HttpNotFound();
            }
            return View(trabalho);
        }

        // GET: Trabalho/Create
        public ActionResult Create()
        {
            ViewBag.AutorID = new SelectList(db.Autor, "Id", "Nome");
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome");
            var trabalho = new Trabalho();
            trabalho.Autor = new List<Autor>();
            PopulateAssignedAutorData(trabalho);
            return View();
        }



        // POST: Trabalho/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descricao,Data_Publicacao,Grau_Academico,Estado,DirectorioDoc,AreaID")] Trabalho trabalho,string[] selectedAutores,HttpPostedFileBase ficheiro)
        {
            UserCredential credential;
            var path = Server.MapPath(@"~/Scripts/client_secret.json");
            using (var stream =
                new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/drive-dotnet-Demo");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            if (selectedAutores != null)
            {
                trabalho.Autor = new List<Autor>();
                foreach (var autor in selectedAutores)
                {
                    var autorAdd = db.Autor.Find(int.Parse(autor));
                    trabalho.Autor.Add(autorAdd);
                }
            }

            trabalho.Estado = "Registado";
            trabalho.Data_Publicacao = DateTime.Now.ToString();
            if (ModelState.IsValid)
            {

                Google.Apis.Drive.v2.Data.File body = new Google.Apis.Drive.v2.Data.File();
                body.Title = ficheiro.FileName;
                body.Description = ficheiro.FileName;
                Debug.WriteLine(ficheiro.GetType().ToString());
                Debug.WriteLine(ficheiro.ContentType);
                body.MimeType = "application/pdf";
                var parentId = "0BwiuFVfOZIECU0w1dnA0WlR6UkU";
                if (!String.IsNullOrEmpty(parentId))
                {
                    body.Parents = new List<ParentReference>()
          {new ParentReference() {Id = parentId}};
                }
                // File's content.
                byte[] byteArray = System.IO.File.ReadAllBytes("/GuiaPagamentoReport.pdf");
                MemoryStream streamf = new MemoryStream(byteArray);
                try
                {
                    FilesResource.InsertMediaUpload request = service.Files.Insert(body, streamf, "application/pdf");
                    request.Upload();

                    Google.Apis.Drive.v2.Data.File file = request.ResponseBody;

                    // Uncomment the following line to print the File ID.
                    Debug.WriteLine("File ID: " + file.Id);
                    trabalho.DirectorioDoc = file.Id;

                }
                catch (Exception e)
                {
                    Debug.WriteLine("An error occurred: " + e.Message);
                }

                //                var fileName = Path.GetFileName(file.FileName);
                //              var path = Path.Combine(Server.MapPath("~/App_Data/Uploads"), fileName);
                //            file.SaveAs(path);
                //          trabalho.DirectorioDoc =path;
                db.Trabalho.Add(trabalho);
                db.SaveChanges();
                return null;//RedirectToAction("Index");
            }



            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }
        public static Google.Apis.Drive.v2.Data.File updateFile(DriveService service, string _uploadFile)
        {
            Google.Apis.Drive.v2.Data.File file = new Google.Apis.Drive.v2.Data.File();
            byte[] byteArray = System.IO.File.ReadAllBytes(_uploadFile);
            System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
            FilesResource.InsertMediaUpload request = service.Files.Insert(file, stream, "application/pdf");

            request.Upload();
            Google.Apis.Drive.v2.Data.File updatedFile = request.ResponseBody;
            return updatedFile;
        }
        // GET: Trabalho/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            if (trabalho == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }

        // POST: Trabalho/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descricao,Data_Publicacao,Grau_Academico,Data_Defesa,Estado,DirectorioDoc,AreaID")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trabalho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Area, "Id", "Nome", trabalho.AreaID);
            return View(trabalho);
        }

        // GET: Trabalho/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trabalho trabalho = db.Trabalho.Find(id);
            if (trabalho == null)
            {
                return HttpNotFound();
            }
            return View(trabalho);
        }

        // POST: Trabalho/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trabalho trabalho = db.Trabalho.Find(id);
            db.Trabalho.Remove(trabalho);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedAutorData(Trabalho trabalho)
        {
            var allAutors = db.Autor;
            var trabalhoAutores = new HashSet<int>(trabalho.Autor.Select(c => c.Id));
            var viewModel = new List<AssignedAutorData>();
            foreach (var autor in allAutors)
            {
                viewModel.Add(new AssignedAutorData
                {
                    AutorID = autor.Id,
                    Nome = autor.Nome,
                    Assigned = trabalhoAutores.Contains(autor.Id)
                });
            }
            ViewBag.Autores = viewModel;
        }
    }
}
