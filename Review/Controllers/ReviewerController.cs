using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Review.Model;
using Review.Model.DTO;
using Review.Model.Interfaces;
using Review.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Threading.Tasks;

namespace Review.Controllers {
    [Authorize]
    public class ReviewerController : Controller {
        private readonly IReviewerService _reviewerService;
        private readonly ILogger<ReviewerController> _logger;

        public string AlertMessage {
            get { return TempData["AlertMessage"].ToString(); }
            set { TempData["AlertMessage"] = value; }
        }

        public ReviewerController( IReviewerService reviewerService, ILogger<ReviewerController> logger ) {
            _reviewerService = reviewerService;
            _logger = logger;
        }

        public async Task<IActionResult> Index() {
            try {
                IEnumerable<Reviewer> reviewers = await _reviewerService.GetAllReviewersAsync();
                return View( reviewers.ToList() );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while getting all reviewers." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpGet]
        public IActionResult Create() {
            try {
                return View();
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the create view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create( Reviewer reviewer ) {
            try {
                if( ModelState.IsValid ) {
                    bool isReviewerCreated = await _reviewerService.CreateReviewerAsync( reviewer );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( reviewer );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while creating a reviewer." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpGet, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id ) {
            try {
                var reviewer = await _reviewerService.GetReviewerByIdAsync( id );
                if( reviewer == null ) {
                    return NotFound();
                }
                return View( reviewer );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the edit view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpPost, ActionName( "Edit" )]
        public async Task<IActionResult> Edit( int id, Reviewer reviewer ) {
            try {
                if( ModelState.IsValid ) {
                    var updatedReviewer = await _reviewerService.UpdateReviewerAsync( id, reviewer );
                    return RedirectToAction( nameof( Index ) );
                }

                return View( reviewer );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while editing a reviewer." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details( int id ) {
            try {
                var reviewer = await _reviewerService.GetReviewerByIdAsync( id );
                if( reviewer == null ) {
                    return NotFound();
                }
                return View( reviewer );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while preparing the details view." );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }

        public async Task<IActionResult> Delete( int id ) {
            try {
                await _reviewerService.DeleteReviewerAsync( id );
                return RedirectToAction( "Index" );
            }
            catch( Exception ex ) {
                _logger.LogError( ex, "An error occurred while deleting a reviewer." );
                ModelState.AddModelError( string.Empty, $"Error deleting reviewer: {ex.Message}" );
                return View( "Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
            }
        }
    }
}
