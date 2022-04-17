using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment02NET.Data;
using Assignment02NET.Models;
using Assignment02NET.Models.ViewModels;
using Azure.Storage.Blobs;
using Azure;

namespace Assignment02NET.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly MarketDbContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;
        private readonly string _advertisements = "Advertisements";



        public AdvertisementsController(MarketDbContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        // GET: Advertisements
        public IActionResult Index(string id)

        {



            var Brokerages = _context.Brokerages.Where(m => m.ID == id).FirstOrDefault();
            var AdsViewModel = new AdsViewModel();
            AdsViewModel.Brokerages = Brokerages;
            var AdvertisementsBrokerages = _context.Advertisements.Where(i => i.BrokerageId == id).Include(i => i.Brokerage).ToList();
            AdsViewModel.Advertisements = AdvertisementsBrokerages;

            return View(AdsViewModel);

        }


        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements
                .Include(a => a.Brokerage)
                .FirstOrDefaultAsync(m => m.advertisementID == id);
            if (advertisements == null)
            {
                return NotFound();
            }

            return View(advertisements);
        }

        // GET: Advertisements/Create
        public IActionResult Create()
        {
            var Brokerages = _context.Brokerages.Include(i => i.Advertisements).FirstOrDefault();

            ViewData["BrokerageId"] = new SelectList(_context.Brokerages, "ID", "ID");
            return View(Brokerages);
        }



        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IEnumerable<IFormFile> files)
        {



            try
            {
                _blobContainerClient = await _blobServiceClient.CreateBlobContainerAsync(_advertisements);
                _blobContainerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_advertisements);
            }
            try
            {
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        //var filePath = Path.Combine(_config["StoredFilesPath"],Path.GetRandomFileName());
                        var filePath = Path.GetRandomFileName();
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                            stream.Position = 0;
                            await _blobContainerClient.GetBlobClient(filePath).UploadAsync(stream);
                            stream.Close();
                        }
                        var image = new Advertisements
                        {
                            Url = _blobContainerClient.GetBlobClient(filePath).Uri.AbsoluteUri,
                            FileName = filePath,
                        };
                        _context.Advertisements.Add(image);
                        await _context.SaveChangesAsync();

                    }

                }

            }
            catch (RequestFailedException)
            {

                return RedirectToAction("Error");
            }

            return RedirectToAction("Index");
        }
        /* public async Task<IActionResult> Create([Bind("advertisementID,FileName,Url,BrokerageId")] Advertisements advertisements)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(advertisements);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["BrokerageId"] = new SelectList(_context.Brokerages, "ID", "ID", advertisements.BrokerageId);
             return View(advertisements);
         }*/

        // GET: Advertisements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements.FindAsync(id);
            if (advertisements == null)
            {
                return NotFound();
            }
            ViewData["BrokerageId"] = new SelectList(_context.Brokerages, "ID", "ID", advertisements.BrokerageId);
            return View(advertisements);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("advertisementID,FileName,Url,BrokerageId")] Advertisements advertisements)
        {
            if (id != advertisements.advertisementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementsExists(advertisements.advertisementID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrokerageId"] = new SelectList(_context.Brokerages, "ID", "ID", advertisements.BrokerageId);
            return View(advertisements);
        }

        // GET: Advertisements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var advertisements = await _context.Advertisements
                .Include(a => a.Brokerage)
                .FirstOrDefaultAsync(m => m.advertisementID == id);
            if (advertisements == null)
            {
                return NotFound();
            }

            return View(advertisements);
        }

        // POST: Advertisements/Delete/5

        /*     public async Task<IActionResult> DeleteConfirmed(int id)
             {
                 var advertisements = await _context.Advertisements.FindAsync(id);
                 _context.Advertisements.Remove(advertisements);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
        */
        private bool AdvertisementsExists(int id)
             {
                 return _context.Advertisements.Any(e => e.advertisementID == id);
             }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var Advertisements = await _context.Advertisements.FindAsync(id);


            if (Advertisements != null)
            {
                try
                {
                    _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_advertisements);
                    if (await _blobContainerClient.GetBlobClient(Advertisements.FileName).ExistsAsync())
                    {
                        await _blobContainerClient.GetBlobClient(Advertisements.FileName).DeleteAsync();
                    }
                    _context.Advertisements.Remove(Advertisements);
                    await _context.SaveChangesAsync();
                }


                catch (RequestFailedException)
                {

                    return RedirectToAction("Error");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
