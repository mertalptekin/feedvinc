(function () {
    'use strict';

    /** 
     * video-duzenle
     */
    var $videoBox = $('.video-box');
    var $videoModal = $('#videoModal');
    var $projectVideo = $('#projectVideo');

    $videoModal.on('shown.bs.modal', function (e) {
        $projectVideo[0].play();
    });

    $videoModal.on('hidden.bs.modal', function (e) {
        $projectVideo[0].pause();
    });

    /** 
     * projem
     */
    var $projemVideosSpan = $('.projem-videos--span');
    var $projemVideoModal = $('#projemVideoModal');
    var $projemVideo = $('#projemVideo');

    $projemVideoModal.on('shown.bs.modal', function (e) {
        $projemVideo[0].play();
    });

    $projemVideoModal.on('hidden.bs.modal', function (e) {
        $projemVideo[0].pause();
    });

    /** 
     * proje-hakkinda
     */
    var $projeHakkindaVideosSpan = $('.projeHakkinda-videos--span');
    var $projeHakkindaVideoModal = $('#projeHakkindaVideoModal');
    var $projeHakkindaVideo = $('#projeHakkindaVideo');

    $projeHakkindaVideoModal.on('shown.bs.modal', function (e) {
        $projeHakkindaVideo[0].play();
    });

    $projeHakkindaVideoModal.on('hidden.bs.modal', function (e) {
        $projeHakkindaVideo[0].pause();
    });

})();