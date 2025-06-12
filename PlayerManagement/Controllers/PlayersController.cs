using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerManagement.Models;
using PlayerManagement.Models.ViewModels;

namespace PlayerManagement.Controllers
{
    public class PlayersController : Controller
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment web;

        public PlayersController(AppDbContext db, IWebHostEnvironment web)
        {
            this.db = db;
            this.web = web;
        }

        public IActionResult Index()
        {
            IEnumerable<Player> player = db.Players.Include(c => c.Citizenships).Include(p => p.PlayerSkills).ToList();
            return View(player);
        }
        public IActionResult CreatePlayer()
        {
            PlayerViewModels player = new PlayerViewModels();
            player.Citizenships = db.Citizenships.ToList();
            player.PlayerSkills.Add(new PlayerSkill { PlayerSkillId = 1 });
            return PartialView("_CreatePlayer",player);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePlayer(PlayerViewModels vObj)
        {
            if (!ModelState.IsValid)
            {
                vObj.Citizenships = db.Citizenships.ToList();
                return View();
            }
            Player obj = new Player
            {
                PlayerId = vObj.PlayerId,
                PlayerName = vObj.PlayerName,
                MobileNo = vObj.MobileNo,
                Email = vObj.Email,
                IsOverseas = vObj.IsOverseas,
                CitizenshipId = vObj.CitizenshipId,
                SigningDate = vObj.SigningDate,
                SigningFee = vObj.SigningFee,
                PlayerSkills = vObj.PlayerSkills
            };
            if (vObj.ProfileFile != null)
            {
                string fileName = GetFileName(vObj.ProfileFile);
                obj.ImageUrl = fileName;
            }
            else
            {
                obj.ImageUrl = "noimage.jpg";
            }
            db.Players.Add(obj);
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                vObj.Citizenships = db.Citizenships.ToList();
                return View();
            }
        }

        private string GetFileName(IFormFile profileFile)
        {
            string uFileName = null;
            if (profileFile != null)
            {
                uFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName);
                string uploadFolder = Path.Combine(web.WebRootPath, "images");
                string filePath = Path.Combine(uploadFolder, uFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profileFile.CopyToAsync(fileStream);
                }
            }
            return uFileName;
        }

        public IActionResult DeletePlayer(int id)
        {
            Player player = db.Players.Find(id);
            if (player!= null)
            {
                db.Players.Remove(player);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditPlayer(int id)
        {
            var player = db.Players.Include(p => p.PlayerSkills).FirstOrDefault(x => x.PlayerId == id);
            var vObj = new PlayerViewModels
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                MobileNo = player.MobileNo,
                Email = player.Email,
                IsOverseas = player.IsOverseas,
                CitizenshipId = player.CitizenshipId,
                SigningDate = player.SigningDate,
                SigningFee = player.SigningFee,
                ImageUrl = player.ImageUrl,
                PlayerSkills = player.PlayerSkills.ToList(),
                Citizenships = db.Citizenships.ToList()
            };
            return PartialView("_EditPlayer", vObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPlayer(PlayerViewModels vObj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vObj.Citizenships = db.Citizenships.ToList();
                return View();
            }
            Player obj = db.Players.FirstOrDefault(x => x.PlayerId == vObj.PlayerId);
            if (obj != null)
            {
                obj.PlayerId = vObj.PlayerId;
                obj.PlayerName = vObj.PlayerName;
                obj.Email = vObj.Email;
                obj.MobileNo = vObj.MobileNo;
                obj.IsOverseas = vObj.IsOverseas;
                obj.CitizenshipId = vObj.CitizenshipId;
                obj.SigningFee= vObj.SigningFee;
                obj.SigningDate = vObj.SigningDate;
            }
            if (vObj.ProfileFile != null)
            {
                string fileName = GetFileName(vObj.ProfileFile);
                obj.ImageUrl = fileName;
            }
            else
            {
                obj.ImageUrl = OldImageUrl;
            }
            var skills = db.PlayerSkills.Where(x => x.PlayerId == vObj.PlayerId).ToList();
            if (skills!= null)
            {
                db.PlayerSkills.RemoveRange(skills);
            }
            if (vObj.PlayerSkills != null)
            {
                foreach (var item in vObj.PlayerSkills)
                {
                    item.PlayerId = obj.PlayerId;
                    item.SkillName = item.SkillName;
                    item.SkillLevel = item.SkillLevel;
                    db.PlayerSkills.Add(item);
                }
            }
            db.Entry(obj).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                vObj.Citizenships = db.Citizenships.ToList();
                return View();
            }
        }
    }
}
