(function($) {

	"use strict";

	var fullHeight = function() {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function(){
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$('#sidebarCollapse').on('click', function () {
      $('#sidebar').toggleClass('active');
  });

})(jQuery);

document.addEventListener("DOMContentLoaded", () => {
	let deletebuttompage = document.querySelectorAll("#delete");
	deletebuttompage.forEach(elem => {
		elem.addEventListener("click", () => {
			console.log(elem.getAttribute("data-myvalue"));
			let deletebuttonmodal = document.querySelector('#comfirmDelete');
			deletebuttonmodal.setAttribute('value', elem.getAttribute("data-myvalue"));
		})
	})


});

