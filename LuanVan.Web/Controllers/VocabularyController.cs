using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LuanVan.Web.Models.Domain;
using System.IO;
using LuanVan.Web.Models.ViewModel;
using QRCoder;
using System.Drawing;
using LuanVan.Web.Support;

namespace LuanVan.Web.Controllers
{
    [LVAuthorize(RoleType.Teacher)]
    public class VocabularyController : Controller
    {
        private LuanVanDbContext db = new LuanVanDbContext();

        //
        // GET: /Vocabulary/Details/5

        public ActionResult Details(int id = 0)
        {
            var vocabulary = db.Vocabularies.Find(id);
            if (vocabulary == null)
            {
                return HttpNotFound();
            }
            return View(vocabulary);
        }

        //
        // GET: /Vocabulary/Create

        public ActionResult Create(int groupId)
        {
            var response = new CreateVocabularyViewModel 
            {
                GroupId = groupId
            };

            return View(response);
        }

        //
        // POST: /Vocabulary/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateVocabularyViewModel request)
        {
            if (request.ImageFile == null)
            {
                ModelState.AddModelError("", "Image is required.");
                return View(request);
            }

            if (ModelState.IsValid)
            {
                var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                var imageExtension = Path.GetExtension(request.ImageFile.FileName);

                var imageName = imageId + imageExtension;
                string pic = System.IO.Path.GetFileName(imageName);

                string folderPath = "/img/vocabulary";
                Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                request.ImageFile.SaveAs(path);


                var group = db.Groups.Find(request.GroupId);

                var vocabulary = new Vocabulary
                {
                    Word = request.Word,
                    //Lesson = lesson
                    ImageId = imageId,
                    ImageExtension = imageExtension,
                    Group = group
                };

                db.Vocabularies.Add(vocabulary);

                db.SaveChanges();

                return RedirectToAction("Details", "Group", new { Id = request.GroupId });
            }

            return View(request);
        }

        public ActionResult GenerateQRCode(string imageId)
        {
            //var jsonString = imageId;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(imageId, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            var bitmapBytes = BitmapToBytes(qrCodeImage); //Convert bitmap into a byte array
            return File(bitmapBytes, "image/jpeg"); //Return as file result
        }

        private static byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public ActionResult Edit(int id = 0)
        {
            var vocabulary = db.Vocabularies.Find(id);
            if (vocabulary == null)
            {
                return HttpNotFound();
            }

            var response = new EditVocabularyViewModel
            {
                Id = vocabulary.Id,
                Word = vocabulary.Word,
                ImageUrl = vocabulary.ImageUrl,
                GroupId = vocabulary.Group.Id
            };

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditVocabularyViewModel request)
        {
            if (ModelState.IsValid)
            {
                var vocabulary = db.Vocabularies.Find(request.Id);
                vocabulary.Word = request.Word;

                if (request.ImageFile != null)
                {
                    var imageId = Guid.NewGuid().ToString().Replace("-", "").ToLower();
                    var imageExtension = Path.GetExtension(request.ImageFile.FileName);
                    var imageName = imageId + imageExtension;
                    string pic = System.IO.Path.GetFileName(imageName);

                    string folderPath = "/img/vocabulary";
                    Directory.CreateDirectory(Server.MapPath("~" + folderPath));
                    string path = System.IO.Path.Combine(Server.MapPath("~" + folderPath), pic);
                    request.ImageFile.SaveAs(path);

                    vocabulary.ImageId = imageId;
                    vocabulary.ImageExtension = imageExtension;
                }

                db.SaveChanges();
                return RedirectToAction("Details", "Group", new { Id = vocabulary.Group.Id });
            }
            return View(request);
        }

        public ActionResult Delete(int id = 0)
        {
            var vocabulary = db.Vocabularies.Find(id);
            if (vocabulary == null)
            {
                return HttpNotFound();
            }

            var response = new DeleteVocabularyViewModel
            {
                Id = vocabulary.Id,
                Word = vocabulary.Word,
                ImageUrl = vocabulary.ImageId,
                GroupId = vocabulary.Group.Id
            };

            return View(response);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(DeleteVocabularyViewModel request)
        {
            var vocabulary = db.Vocabularies.Find(request.Id);
            int groupId = vocabulary.Group.Id;
            try
            {
                db.Vocabularies.Remove(vocabulary);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Details", "Group", 
                    new { Id = groupId, error = "Can't delete this vocabulary. Some students are learning it." });
            }

            return RedirectToAction("Details", "Group", new { Id = groupId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}