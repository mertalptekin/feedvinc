var timer = null,
	slideTime = 20000;

$(document).on('ready', function () {

	$(".slide-btn").click(function() {
		fullscreenSlideStop();
		var currSlide = $(this).data("slide-num");
		$(".fullscreen-slider li").removeClass("active");
		$("[data-slide-number=" + currSlide + "]").addClass("active");
		fullscreenSlideStart();			
	});
});

function fullscreenSlideStart() {
	timer = setInterval(fullscreenSlide, slideTime);
}

function fullscreenSlideStop() {
	clearInterval(timer);
}

function fullscreenSlideNav(direction, el) {
	var currSlide = $(".fullscreen-slider li.active").data("slide-number"),
		$slides = $(".fullscreen-slider li"),
		$firstSlide = $("[data-slide-number]").first(),
		$lastSlide = $("[data-slide-number]").last();

	fullscreenSlideStop();

	if (direction == "next") {
		if ($lastSlide.hasClass("active")) {
			$lastSlide.removeClass("active");
			$firstSlide.addClass("active");		
		} else {
			var nextSlide = currSlide + 1;
			$slides.removeClass("active");
			$("[data-slide-number=" + nextSlide + "]").addClass("active");
			//fullscreenSlideStart();
		}
	}
	if (direction == "prev") {
		if ($firstSlide.hasClass("active")) {
			$firstSlide.removeClass("active");
			$lastSlide.addClass("active");		
		} else {
			var nextSlide = currSlide - 1;
			$slides.removeClass("active");
			$("[data-slide-number=" + nextSlide + "]").addClass("active");
			//fullscreenSlideStart();
		}
	}
}

function fullscreenSlide() {
	var $firstSlide = $("[data-slide-number=1]"),
		$lastSlide = $("[data-slide-number=5]"),
		$currSlide = $(".fullscreen-slider li.active");
	
	if ($lastSlide.hasClass("active")) {
		$lastSlide.removeClass("active");
		$firstSlide.addClass("active");		
	}

	$currSlide.removeClass("active").next().addClass("active");
}