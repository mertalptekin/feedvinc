/* ********************** */
/* 		notifyMe 0.2 	  */
/* Developed by Cihan Bir */
/* ********************** */

var notifyMe = (function(){
		var 
		method = {},
		$modalWrap,
		$modal,
		$content,
		$close;
		
		method.open = function (settings) {
			$content.empty().append(settings.content);
			$modal.removeClass("error");
			$modal.removeClass("success");
			$modal.removeClass("alert-show");
			$modalWrap.removeClass("opened");			
			$modal.addClass(settings.type);
			$modal.append($close);
			$modalWrap.addClass("opened");
			$modal.addClass("alert-show");
			
			if (settings.delay) {
				setTimeout(modalClose, settings.delay);
			}
		};
		
		method.close = function () {
			$modalWrap.removeClass("opened");
			$modal.removeClass("alert-show");
		};
		
		method.isOpen = function () {
			return $modal.hasClass("alert-show");
		};
		
		$modalWrap = $('<div class="notifyMe-wrap"></div>');
		$modal = $('<div class="notifyMe"></div>');
		$content = $('<div class="contents"></div>');
		$close = $('<button type="button" class="alert-close"><i class="fa fa-close"></i></button>');
		
		$modalWrap.append($modal);
		$modal.append($content, $close);
		
		$(document).ready(function(){
			$('body').append($modalWrap);
									
		});
		
		$close.click(function(e){
			e.preventDefault();
			method.close();
		});
		function modalClose() {
			method.close();
		}
		
		return method;
}());