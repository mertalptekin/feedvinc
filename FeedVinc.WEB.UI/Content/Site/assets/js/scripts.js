(function(){
    var windowWidth = $("body").width(),
        isShareAttached = false;

    if ($('.image-group').length || $('.image-link').length || $('.magnific-inline').length ) {
        $.extend(true, $.magnificPopup.defaults, {
          tClose: 'Kapat (Esc)',
          tLoading: 'Yükleniyor...',
          gallery: {
            tPrev: 'Geri (Sol ok tuşu)',
            tNext: 'İleri (Sağ ok tuşu)',
            tCounter: '%curr% / %total%'
          },
          image: {
            tError: '<a href="%url%">Bu resim</a> yüklenemiyor.'
          },
          inline: {
            tError: '<a href="%url%">Bu içerik</a> yüklenemiyor.'
          },
          ajax: {
            tError: '<a href="%url%">Bu içerik</a> yüklenemiyor.'
          }
        });
    }
    if ($(".feed-stars").length) {
        $(".feed-stars").raty({
            half: true,
            score: 4.5,
            readOnly: true,
            hints: [null, null, null, null, null]
        });
    }
    if ($(".project-stars").length) {
        $(".project-stars").raty({
            half: true,
            hints: [null, null, null, null, null]
        });
    }
    if ($(".reports-table").length) {
        var sum = 0;
        var sum2 = 0;

        $('.sale-price').each(function() {
            var price = $(this);
            sum += parseFloat(price.html());
        });

        $('.expense-price').each(function() {
            var price = $(this);
            sum2 += parseFloat(price.html());
        });

        if (!isNaN(sum)) {
            sum = sum.toFixed(3);
            sum = sum + ".00";
            if (!sum == "0.000.00") {
                $('.sale-sum').html(sum);
            }
        }
        if (!isNaN(sum2)) {
            sum2 = sum2.toFixed(3);
            sum2 = sum2 + ".00";

            if (!sum2 == "0.000.00") {
                $('.expense-sum').html(sum2);
            }
        }
    }
    
    if($('#income_chart').length) {
        var config = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        8000,
                        12000,
                        30000,
                        40000,
                        50000,
                    ],
                    backgroundColor: [
                        "#f9506d",
                        "#6332b3",
                        "#c4d839",
                        "#289df9",
                        "#edb554",
                    ],
                }],
                labels: [
                    "Nisan 2016",
                    "Mayıs 2016",
                    "Haziran 2016",
                    "Temmuz 2016",
                    "Ağustos 2016"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i] + 'TL');
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

       
        var ctx = document.getElementById("income_chart").getContext("2d");
        window.myDoughnut = new Chart(ctx, config);
        var legend = myDoughnut.generateLegend();
        $('#income-chart').append(legend);

    }
    if($('#outcome_chart').length) {
        var config2 = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        8000,
                        15000,
                        35000,
                        20000,
                        20000,
                    ],
                    backgroundColor: [
                        "#f9506d",
                        "#6332b3",
                        "#c4d839",
                        "#289df9",
                        "#edb554",
                    ],
                }],
                labels: [
                    "Nisan 2016",
                    "Mayıs 2016",
                    "Haziran 2016",
                    "Temmuz 2016",
                    "Ağustos 2016"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i] + 'TL');
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

        
        var ctx = document.getElementById("outcome_chart").getContext("2d");
        window.myDoughnut2 = new Chart(ctx, config2);
        var legend2 = myDoughnut2.generateLegend();
        $('#outcome-chart').append(legend2);
    }

    if($('#ppa_follower').length) {
        var config7 = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        200,
                        10,
                        150,
                        350,
                        90,
                    ],
                    backgroundColor: [
                        "#f9506d",
                        "#6332b3",
                        "#c4d839",
                        "#289df9",
                        "#edb554"
                    ],
                }],
                labels: [
                    "Girişimci",
                    "Yatırımcı",
                    "Yazılımcı",
                    "Kullanıcı",
                    "Diğer"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                cutoutPercentage: 60,
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list on-side">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i]);
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

        
        var ctx = document.getElementById("ppa_follower").getContext("2d");
        window.myDoughnut7 = new Chart(ctx, config7);
        var legend5 = myDoughnut7.generateLegend();
        $('#ppa-follower').append(legend5);
    }

    if($('#ppa_follower_gender').length) {
        var config8 = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        650,
                        150
                    ],
                    backgroundColor: [
                        "#289df9",
                        "#f9506d"
                    ],
                }],
                labels: [
                    "Erkek",
                    "Kadın"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                cutoutPercentage: 60,
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list on-side">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i]);
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

        
        var ctx = document.getElementById("ppa_follower_gender").getContext("2d");
        window.myDoughnut8 = new Chart(ctx, config8);
        var legend6 = myDoughnut8.generateLegend();
        $('#ppa-follower-gender').append(legend6);
    }

    if($('#ppa_profile').length) {
        var config9 = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        10,
                        2,
                        15,
                        8,
                        2,
                    ],
                    backgroundColor: [
                        "#f9506d",
                        "#6332b3",
                        "#c4d839",
                        "#289df9",
                        "#edb554"
                    ],
                }],
                labels: [
                    "Girişimci",
                    "Yatırımcı",
                    "Yazılımcı",
                    "Kullanıcı",
                    "Diğer"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                cutoutPercentage: 60,
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list on-side">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i]);
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

        
        var ctx = document.getElementById("ppa_profile").getContext("2d");
        window.myDoughnut9 = new Chart(ctx, config9);
        var legend7 = myDoughnut9.generateLegend();
        $('#ppa-profile').append(legend7);
    }

    if($('#ppa_profile_gender').length) {
        var config10 = {
            type: 'doughnut',
            data: {
                datasets: [{
                    data: [
                        32,
                        8
                    ],
                    backgroundColor: [
                        "#289df9",
                        "#f9506d"
                    ],
                }],
                labels: [
                    "Erkek",
                    "Kadın"
                ]
            },
            options: {
                responsive: true,
                legend: false,
                animation: {
                    animateRotate: true
                },
                cutoutPercentage: 60,
                legendCallback: function(chart) {
                    var text = [];
                    text.push('<ul class="chart-list on-side">');
                    for (var i=0; i<chart.data.datasets[0].data.length; i++) {
                        text.push('<li>');
                        text.push('<span style="color:' + chart.data.datasets[0].backgroundColor[i] + '">' + chart.data.labels[i] + ':&nbsp;</span>');
                        text.push(chart.data.datasets[0].data[i]);
                        text.push('</li>');
                    }
                    text.push('</ul>');
                    return text.join("");
                }
            }
        };

        
        var ctx = document.getElementById("ppa_profile_gender").getContext("2d");
        window.myDoughnut10 = new Chart(ctx, config10);
        var legend8 = myDoughnut10.generateLegend();
        $('#ppa-profile-gender').append(legend8);
    }

    if($('.metric-donut-chart').length) {
        $('.metric-donut-chart').easyPieChart({
            easing: 'easeInQuad',
            size: '110',
            barColor: '#db9e36',
            trackColor: '#419ba9',
            scaleColor: false,
            lineWidth: 20,
            trackWidth: 20,
            lineCap: 'butt',
        });
    }

    if ($('#financial_line_chart').length) {

        var config4 = {
            type: 'line',
            data: {
                labels: ["Nisan '16", "Mayıs '16", "Haziran '16", "Temmuz '16", "Ağustos '16"],
                datasets: [{
                    label: "",
                    data: [100, 200, 170, 250, 300],
                    fill: false,
                    lineTension: 0,
                    borderColor: "#419ba9",
                    pointBackgroundColor: "#419ba9",
                    borderWidth: 2,
                    pointBorderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: false,
                tooltips: {
                    mode: 'label',
                },
                hover: {
                    mode: 'dataset'
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        },
                        ticks: {
                            suggestedMin: 100,
                            suggestedMax: 300,
                        }
                    }]
                }
            }
        };
        
        var ctx = document.getElementById("financial_line_chart").getContext("2d");
        window.myLine = new Chart(ctx, config4);
        
    }

    if ($('#metrik_line_chart').length) {

        var config5 = {
            type: 'line',
            data: {
                labels: ["Nisan '16", "Mayıs '16", "Haziran '16", "Temmuz '16", "Ağustos '16"],
                datasets: [{
                    label: "",
                    data: [1000, 2000, 1700, 2500, 3000],
                    fill: false,
                    lineTension: 0,
                    borderColor: "#419ba9",
                    pointBackgroundColor: "#419ba9",
                    borderWidth: 2,
                    pointBorderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: false,
                tooltips: {
                    mode: 'label',
                },
                hover: {
                    mode: 'dataset'
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        },
                        ticks: {
                            suggestedMin: 1000,
                            suggestedMax: 6000,
                        }
                    }]
                }
            }
        };
        
        var ctx = document.getElementById("metrik_line_chart").getContext("2d");
        window.myLine2 = new Chart(ctx, config5);
        
    }

    if ($('#ads_line_chart').length) {

        var config11 = {
            type: 'line',
            data: {
                labels: ["Nisan '16", "Mayıs '16", "Haziran '16", "Temmuz '16", "Ağustos '16"],
                datasets: [{
                    label: "",
                    data: [1000, 2000, 1700, 2500, 3000],
                    fill: false,
                    lineTension: 0,
                    borderColor: "#419ba9",
                    pointBackgroundColor: "#419ba9",
                    borderWidth: 2,
                    pointBorderWidth: 1
                }]
            },
            options: {
                responsive: true,
                legend: false,
                tooltips: {
                    mode: 'label',
                },
                hover: {
                    mode: 'dataset'
                },
                scales: {
                    xAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        }
                    }],
                    yAxes: [{
                        display: true,
                        scaleLabel: {
                            display: true,
                            labelString: ''
                        },
                        ticks: {
                            suggestedMin: 1000,
                            suggestedMax: 6000,
                        }
                    }]
                }
            }
        };
        
        var ctx = document.getElementById("ads_line_chart").getContext("2d");
        window.myLine3 = new Chart(ctx, config11);
        
    }

    if ($(".message-row").length) {
    	$(".message-row").mCustomScrollbar({
            theme:"minimal",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".mentor-chat-box").length) {
        $(".mentor-chat-box").mCustomScrollbar({
            theme:"minimal",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".user-scrollbar").length) {    
        $(".user-scrollbar").mCustomScrollbar({
            theme:"minimal",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".campaign-modal-content").length) {    
        $(".campaign-modal-content").mCustomScrollbar({
            theme:"light-thick",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".notifications-modal").length) {    
        $(".notifications-modal").mCustomScrollbar({
            theme:"light-thick",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".avatar-bottom").length) {    
        $(".avatar-bottom").mCustomScrollbar({
            theme:"dark-3",
            axis: "x",
            advanced:{ 
                updateOnContentResize: true,
                autoExpandHorizontalScroll:true
            }
        });
    }
    if ($("#secili_projeler").length) {
        $('#secili_projeler').tagsinput({
            cancelConfirmKeysOnEmpty: true,
            maxTags: 10,
            itemValue: 'text'
        });

        $('#secili_projeler').on('itemRemoved', function(event) {
            var projectID = event.item.id,
                items = $('#secili_projeler').tagsinput('items'),
                $investerBtn = $('[data-project-id='+ projectID +']'),
                curr = $('.active-projects').html(),
                currInvesterNum = parseInt(curr),
                one = 1,
                index = 0,
                investerNum = currInvesterNum - one;
            
            $investerBtn.removeClass('disabled').prop('disabled', '').html('Projeyi Seç');
            $('.active-projects').html(investerNum);

            if (items.length <= index) {
                $('.report-comparison').removeClass('active');
                $('.project-comparison-btn').removeClass('aktif');
            }

        });
    }
    if ($("#project_tags").length) {
        $('#project_tags').tagsinput({
            cancelConfirmKeysOnEmpty: true,
            maxTags: 5
        });
    }
    if ($("#invester_tags").length) {
        $('#invester_tags').tagsinput({
            itemValue: 'text'
        });

        $('#invester_tags').on('itemRemoved', function(event) {
            var investerID = event.item.id,
                items = $('#invester_tags').tagsinput('items'),
                $investerDiv = $('[data-invester-id='+ investerID +']'),
                curr = $('.active-invester').html(),
                currInvesterNum = parseInt(curr),
                one = 1,
                index = 0,
                investerNum = currInvesterNum - one;
            
            $investerDiv.find('.choose-invester-btn').removeClass('disabled').prop('disabled', '').html('Seç');
            $('.active-invester').html(investerNum);

            if (items.length <= index) {
                $('.sn-active-investers').removeClass('active');
            }

        });
    }
    
    if ($(".canvas-modal-content").length) {    
        $(".canvas-modal-content").mCustomScrollbar({
            theme:"inset-3",
            advanced:{ updateOnContentResize: true }
        });
    }
    if ($(".inputfile").length) {
        $(".inputfile").nicefileinput({ 
            label : 'Fotoğraf Yükle'
        });
    }
    if ($(".add-image").length) {
        $(".add-image").nicefileinput({ 
            label : 'Resim Ekle'
        });
    }
    if ($(".videofile").length) {
        $(".videofile").nicefileinput({ 
            label : 'Video Yükle'
        });

        $(document).on('change', '.videofile', function(){
            $(this).parents('.NFI-wrapper').prev().removeClass('hidden');
            $(this).parents(".NFI-wrapper").addClass("hidden");
        });
    }
    if ($(".sb-video").length) {
        $(".sb-video").nicefileinput({ 
            label : 'Video Yükle'
        });

        $(document).on('change', '.sb-video', function(){
            $('.attach-box').removeClass('opened');

            $(this).parents('.NFI-wrapper').prev().removeClass('hidden');
            $(this).parents(".NFI-wrapper").addClass("hidden");

            isShareAttached = true;
        });
    }

    if ($(".snVideoEdit").length) {
        $(".snVideoEdit").nicefileinput({ 
            label : 'Farklı Video Yükle'
        });
    }
    if ($('.datepicker').length) {
        $('.datepicker').datetimepicker({
            locale: 'tr',
            viewMode: 'years',
            format: 'DD/MM/YYYY'
        });
    }
    if ($('.project-chart').length) {
        $('.project-chart').easyPieChart({
            easing: 'easeInQuad',
            size: '260',
            barColor: '#db9e36',
            trackColor: '#f2f2f2',
            scaleColor: false,
            lineWidth: 25,
            trackWidth: 25,
            lineCap: 'butt',
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });
    }
    if ($('.task-chart').length) {
        $('.task-chart').easyPieChart({
            easing: 'easeInQuad',
            size: '160',
            barColor: '#db9e36',
            trackColor: '#f2f2f2',
            scaleColor: false,
            lineWidth: 15,
            trackWidth: 15,
            lineCap: 'butt',
            onStep: function(from, to, percent) {
                this.el.children[0].innerHTML = Math.round(percent);
            }
        });
    }
    if ($('.tasks-chart').length) {
        $('.tasks-chart').easyPieChart({
            easing: 'easeInQuad',
            size: '160',
            barColor: '#db9e36',
            trackColor: '#f2f2f2',
            scaleColor: false,
            lineWidth: 15,
            trackWidth: 15,
            lineCap: 'butt',
        });
    }


    if ($('.image-group').length) {

        var magnificPopup = $.magnificPopup.instance;

        $('.image-group').magnificPopup({
            delegate: 'a',
            type: 'image',
            gallery: {
                enabled: true,
                arrowMarkup: '<button title="%title%" type="button" class="mfp-arrow mfp-arrow-%dir%"><i class="fa fa-chevron-%dir%"></i></button>',
                tCounter: '<span class="mfp-pos-top mfp-counter">%curr% / %total%</span>',
            }
        });
    }
    
    if ($('.image-link').length) {
        $('.image-link').magnificPopup({
            type: 'image'
        });
    }

    if ($('#calendar').length) {
        $('#calendar').fullCalendar({
            header: {
                left: '',
                center: 'prev title next',
                right: ''
            },
            allDaySlot: false,
            columnFormat: 'D dddd',
            slotDuration: '00:60:00',
            defaultDate: '2016-08-12',
            defaultView: 'agendaWeek',
            editable: false,
            selectable: false,
            eventOverlap: false,
            events: [
                {
                    id: 1,
                    start: '2016-08-08T10:00:00',
                    end: '2016-08-08T11:00:00',
                    className: 'appointment'
                },
                {
                    id: 2,
                    start: '2016-08-08T11:00:00',
                    end: '2016-08-08T12:00:00',
                    className: 'appointment'
                },
                {
                    id: 3,
                    start: '2016-08-08T12:00:00',
                    end: '2016-08-08T13:00:00',
                    className: 'avaliable'
                },
                {
                    id: 4,
                    start: '2016-08-08T13:00:00',
                    end: '2016-08-08T14:00:00',
                    className: 'avaliable'
                },
                {
                    id: 5,
                    start: '2016-08-08T14:00:00',
                    end: '2016-08-08T15:00:00',
                    className: 'avaliable'
                },
                {
                    id: 6,
                    start: '2016-08-08T15:00:00',
                    end: '2016-08-08T16:00:00',
                    className: 'my-appointment'
                },
                {
                    id: 7,
                    start: '2016-08-09T10:00:00',
                    end: '2016-08-09T11:00:00',
                    className: 'avaliable'
                },
                {
                    id: 8,
                    start: '2016-08-09T11:00:00',
                    end: '2016-08-09T12:00:00',
                    className: 'avaliable'
                },
                {
                    id: 9,
                    start: '2016-08-09T12:00:00',
                    end: '2016-08-09T13:00:00',
                    className: 'avaliable'
                },
                {
                    id: 10,
                    start: '2016-08-09T13:00:00',
                    end: '2016-08-09T14:00:00',
                    className: 'avaliable'
                },
                {
                    id: 11,
                    start: '2016-08-09T14:00:00',
                    end: '2016-08-09T15:00:00',
                    className: 'avaliable'
                },
                {
                    id: 12,
                    start: '2016-08-09T15:00:00',
                    end: '2016-08-09T16:00:00',
                    className: 'avaliable'
                },

            ],
            eventClick: function(calEvent, jsEvent, view) {
                var isAvaliable = $(this).hasClass('avaliable'),
                    eventID = calEvent.id,
                    eventDate = calEvent.start._i,
                    startDate = moment(eventDate).format("D MMMM dddd HH:mm"),
                    $eventDiv = $('.modal-appointment-date');


                if (isAvaliable) {
                    $eventDiv.text(startDate);
                    $eventDiv.attr('data-appointment-date-id', eventID);

                    $('[data-remodal-id=appointment-request-modal]').remodal().open();

                }
            },
            minTime: "08:00:00",
            maxTime: "23:00:00",
            businessHours: {
                dow: [ 0, 1, 2, 3, 4, 5, 6 ],
                start: '08:00',
                end: '23:00',
            },
            contentHeight: 'auto',
            timezone: 'Europe/Istanbul',
        });
    }

    if ($('#calendar_check').length) {
        var myCalendar = $('#calendar_check'); 

        myCalendar.fullCalendar({
            header: {
                left: '',
                center: 'prev title next',
                right: ''
            },
            allDaySlot: false,
            columnFormat: 'D dddd',
            slotDuration: '00:60:00',
            defaultDate: '2016-08-12',
            defaultView: 'agendaWeek',
            editable: false,
            selectable: false,
            eventOverlap: false,
            events: [
                {
                    id: 1,
                    start: '2016-08-08T10:00:00',
                    end: '2016-08-08T11:00:00',
                    className: 'appointment'
                },
                {
                    id: 2,
                    start: '2016-08-08T11:00:00',
                    end: '2016-08-08T12:00:00',
                    className: 'appointment'
                },
                {
                    id: 3,
                    start: '2016-08-08T12:00:00',
                    end: '2016-08-08T13:00:00',
                    className: 'avaliable'
                },
                {
                    id: 4,
                    start: '2016-08-08T13:00:00',
                    end: '2016-08-08T14:00:00',
                    className: 'avaliable'
                },
                {
                    id: 5,
                    start: '2016-08-08T14:00:00',
                    end: '2016-08-08T15:00:00',
                    className: 'avaliable'
                },
                {
                    id: 6,
                    start: '2016-08-08T15:00:00',
                    end: '2016-08-08T16:00:00',
                    className: 'my-appointment'
                },
                {
                    id: 7,
                    start: '2016-08-09T10:00:00',
                    end: '2016-08-09T11:00:00',
                    className: 'avaliable'
                },
                {
                    id: 8,
                    start: '2016-08-09T11:00:00',
                    end: '2016-08-09T12:00:00',
                    className: 'avaliable'
                },
                {
                    id: 9,
                    start: '2016-08-09T12:00:00',
                    end: '2016-08-09T13:00:00',
                    className: 'avaliable'
                },
                {
                    id: 10,
                    start: '2016-08-09T13:00:00',
                    end: '2016-08-09T14:00:00',
                    className: 'avaliable'
                },
                {
                    id: 11,
                    start: '2016-08-09T14:00:00',
                    end: '2016-08-09T15:00:00',
                    className: 'avaliable'
                },
                {
                    id: 12,
                    start: '2016-08-09T15:00:00',
                    end: '2016-08-09T16:00:00',
                    className: 'avaliable'
                },
            ],
            eventClick: function(calEvent, jsEvent, view) {
                var $this = $(this),
                    eventID = calEvent.id,
                    eventDate = calEvent.start._i,
                    startDate = moment(eventDate).format("D MMMM dddd HH:mm");

                if ($this.hasClass('appointment')) { // Randevu Düzenle 
                    
                    var $eventDiv = $('.cancel-appointment-date'),
                        $eventRate = $('.cancel-appointment-rate'); // Tarife verisinin ajax ile çekilmesi gerekiyor.

                    // << ajax on success'de eklenicek >>
                    // $eventRate.text(response);
                    // << ajax on success'de eklenicek >>

                    // on success eklendikten sonra alttaki satırı silin.
                    $eventRate.text("15dk - Ücretsiz");

                    $eventDiv.text(startDate);
                    $eventDiv.attr('data-appointment-date-id', eventID);

                    $('[data-remodal-id=appointment-cancel-modal]').remodal().open();

                } else if ($this.hasClass('my-appointment')) { // Randevu Kabul Et
                    
                    var $eventDiv = $('.accept-appointment-date'),
                        $eventRate = $('.accept-appointment-rate'); // Tarife verisinin ajax ile çekilmesi gerekiyor.


                    // << ajax on success'de eklenicek >>
                    // $eventRate.text(response);
                    // << ajax on success'de eklenicek >>

                     // on success eklendikten sonra alttaki satırı silin.
                    $eventRate.text("15dk - Ücretsiz");

                    $eventDiv.text(startDate);

                    $eventDiv.attr('data-appointment-date-id', eventID);

                    $('[data-remodal-id=appointment-accept-modal]').remodal().open();

                } else if ($this.hasClass('avaliable')) { // Müsait Değil olarak ayarla
                    
                    myCalendar.fullCalendar( 'removeEvents', eventID );

                    //DB'den silinmesi için ajax buraya eklenebilir.

                }
            },
            dayClick: function(date, jsEvent, view) {
                
                var startDate = moment(date).format("YYYY-MM-DDTHH:mm:ss"),
                    hour = date._i[3],
                    newHour = hour + 1,
                    endDate = moment(date).format("YYYY-MM-DDT"+newHour+":mm:ss");
                
                // << ajax ile bu mentor'a ait son randevu id'si alınıcak >>
                // on success >>
                // var eventID = response;
                // << ajax ile bu mentor'a ait son randevu id'si alınıcak >>

                // on success'e eklendikten sonra bir alttaki satırı silin.
                var eventID = 25;

                var myEvent = {
                  id: eventID,
                  start: startDate,
                  end: endDate,
                  className: 'avaliable'
                };

                myCalendar.fullCalendar( 'renderEvent', myEvent, true );
            },
            contentHeight: 'auto',
            timezone: 'Europe/Istanbul',
        });
    }

    if ($('#calendar_view').length) {
        var fullCalendar2 = $('#calendar_view'); 

        fullCalendar2.fullCalendar({
            header: {
                left: '',
                center: '',
                right: 'prev title next'
            },
            allDaySlot: false,
            defaultDate: '2016-09-09',
            defaultView: 'month',
            fixedWeekCount: false,
            editable: false,
            selectable: false,
            contentHeight: 'auto',
            timezone: 'Europe/Istanbul',
            events: [
                {
                    id: 1,
                    start: '2016-09-03',
                    end: '2016-09-03',
                    className: 'personal-event'
                },
                {
                    id: 2,
                    start: '2016-09-07',
                    end: '2016-09-07',
                    className: 'public-event'
                },
                {
                    id: 3,
                    start: '2016-09-09',
                    end: '2016-09-09',
                    className: 'personal-event'
                },
                {
                    id: 4,
                    start: '2016-09-09',
                    end: '2016-09-09',
                    className: 'public-event'
                },
                {
                    id: 5,
                    start: '2016-09-10',
                    end: '2016-09-10',
                    className: 'personal-event'
                },
                {
                    id: 6,
                    start: '2016-09-17',
                    end: '2016-09-17',
                    className: 'public-event'
                },
                {
                    id: 7,
                    start: '2016-09-20',
                    end: '2016-09-20',
                    className: 'public-event'
                },
                {
                    id: 8,
                    start: '2016-09-23',
                    end: '2016-09-23',
                    className: 'personal-event'
                },
                {
                    id: 9,
                    start: '2016-09-23',
                    end: '2016-09-23',
                    className: 'public-event'
                },
                {
                    id: 10,
                    start: '2016-09-28',
                    end: '2016-09-28',
                    className: 'personal-event'
                },
            ],
            dayClick: function(date, jsEvent, view) {
                
                var startDate = moment(date).format("YYYY-MM-DD"),
                    endDate = moment(date).format("YYYY-MM-DD"),
                    dateWithCaption = moment(date).format("DD MMMM YYYY dddd"),
                    $calendarModal = $(".calendar-detail-modal");
  
                $calendarModal.find('#calendarDetailTitle').html(dateWithCaption);
                $calendarModal.find('.add-calendar-btn').attr("data-calendar-date", dateWithCaption);
                $calendarModal.find('.edit-calendar-btn').attr("data-calendar-date", dateWithCaption);

                $('[data-remodal-id=calendar-detail-modal]').remodal().open();
                
            }
        });
    }

    /* Finansal & Metrik Charts */
    if ($('.small-pie-chart').length) {
        $('.small-pie-chart').each(function() {
            var $this = $(this),
                data = $this.data('chart-percent'),
                chartStop = "#db9e36 " + data +"%, #419ba9 0";

            var gradient = new ConicGradient({
                stops: chartStop,
                size: 44
            });
            
            $this.html("<img src='"+ gradient.png +"' />");
        });
    }

    /* on click events */
    $(document).on('click', '.add-calendar-btn', function(e){
        e.preventDefault();

        var eventDate = $(this).data('calendar-date');

        $('[data-remodal-id=calendar-addevent-modal]').attr('data-calendar-date', eventDate);
        $('[data-remodal-id=calendar-addevent-modal]').remodal().open();

    });

    $(document).on('click', '.edit-calendar-btn', function(e){
        e.preventDefault();

        var eventDate = $(this).data('calendar-date');

        $('[data-remodal-id=calendar-editevent-modal]').attr('data-calendar-date', eventDate);
        $('[data-remodal-id=calendar-editevent-modal]').remodal().open();

    });


    $(document).on('click', '.open-messages', function(){
        $('.user-messages').find('.message-preview').hide();
        $('.choose-user').show();
    });

    $(document).on('click', '.home-feed-btn', function(){
        var feedTag = $(this).data('feed-tag');

        //ajax işlemleri burada 


        //ajax işlemlerinden sonra silinebilir
        if (feedTag == "yakin-kampus") {
            $('.feed-tag').removeClass('active');
            $(this).addClass('active');

            $('.feed-home').css("display","block");
            $('.feed-lansman').css("display","none");
            $('.feed-feedback').css("display","none");
        } else if (feedTag == "lansman") {
            $('.feed-tag').removeClass('active');
            $(this).addClass('active');

            $('.feed-home').css("display","none");
            $('.feed-lansman').css("display","block");
            $('.feed-feedback').css("display","none");
        } else if (feedTag == "feedback") {
            $('.feed-tag').removeClass('active');
            $(this).addClass('active');
            
            $('.feed-home').css("display","none");
            $('.feed-lansman').css("display","none");
            $('.feed-feedback').css("display","block");
        }
        
    });

    if ($('.task-todo-edit').length) {
        $('.task-todo-edit').click(function(){
            var $li = $(this).closest('li'),
                dest= $li.offset().top - 10;

            $li.toggleClass('opened');
            $('html,body').animate({scrollTop:dest}, 1000,'swing');
        });
    }
    if ($('.task-todo-cancel').length) {
        $('.task-todo-cancel').click(function(){

            var $li = $(this).closest('li'),
                $txtarea = $li.find('.task-todo-desc'),
                $taskTitle = $li.find('.task-todo-title');


            $li.removeClass('opened');
            $txtarea.val("");
            $taskTitle.val("");
        });
    }

    if ($('.task-todo-save').length) {
        $('.task-todo-save').click(function(){

            var $li = $(this).closest('li'),
                $taskTitle = $li.find('.task-todo-title'), //başlık alanı seçici
                $txtarea = $li.find('.task-todo-desc'), //açıklama alanı seçici
                todoTitle = $taskTitle.val(), //başlığı alıyoruz
                todoDesc = $txtarea.val(), //açıklamayı alıyoruz
                todoID = $li.find('.editable-area').data('task-todo-id'); //database işlemlerinin yapılabilmesi için gerekli id


            if (todoTitle == null || todoTitle == "" || todoDesc == null || todoDesc == "") { 
                notifyMe.open({ 
                    type: "error", 
                    content: "Başlık ve açıklama bölümleri boş bırakılamaz.", 
                    delay: 5000
                });
            } else {
                //ajax işlemleri burda yapılabilir.

                //on success
                $li.find('.task-title').html(todoTitle);
                $li.removeClass('opened');
                $txtarea.val("");
                $taskTitle.val("");

                //aşağıdaki mesajı deneme amaçlı koydum
                var mesaj = todoID + " numaralı id'nin başlığı: " + todoTitle + " açıklaması: " + todoDesc + " olarak güncellenmiştir.";

                notifyMe.open({ 
                    type: "success", 
                    content: mesaj,
                    delay: 7000
                });

            }
                
        });
    }

    $(document).on('click', '.choose-avatar', function(e){
        e.preventDefault();
        
        var avatarID = $(this).data('avatar-id'),
            imgSrc = "assets/images/avatars/" + avatarID + "big.png";


        $('.avatar-list > li').removeClass('choosen');
        $(this).closest('li').addClass('choosen');
        $('.avatar-content').find('p').addClass('disappear');
        $('.avatar-input').val(avatarID);
        $('.avatar').attr('src', imgSrc).addClass('active');
            
    });

    $(document).on('click', '.add-table-column', function(e){
        e.preventDefault();
        $('.table-column').addClass('active');
    });

    $(document).on('click', '.add-column-btn', function(e){
        e.preventDefault();

        var $this = $(this),
            $reportMonth = $('select[name=rapor-ay]'),
            $reportSales = $('input[name=rapor-satis]'),
            $reportExpense = $('input[name=rapor-gider]'),
            reportMonth = $reportMonth.val(),
            reportSales = $reportSales.val(),
            reportExpense = $reportExpense.val(),
            $tr = $('<tr></tr>'),
            $tdF = $('<td></td>'),
            $tdS = $('<td></td>'),
            $tdT = $('<td></td>');

        if (reportMonth == '' || reportSales == '') {
            notifyMe.open({ 
                type: "error", 
                content: "Lütfen rapor için ay ve satış rakamı girin", 
                delay: 3000
            });
        } else {
            var currSale = addCommas(reportSales),
                currExp = addCommas(reportExpense),
                sumSale = currSale +".00";
                sumExp = currExp + ".00";

            $($tr).insertBefore('.reports-table tr.add-column');
            $tr.append($tdF, $tdS, $tdT);
            $tdS.addClass('sale-price');
            $tdT.addClass('expense-price');
            $tdF.html(reportMonth);
            $tdS.html(sumSale);
            $tdT.html(sumExp);
            $reportSales.val("");
            $reportExpense.val("");
            $('.table-column').removeClass('active');
            tableUpdate(reportSales,reportExpense);
        }
    });

    $(document).on('click', '.edit-billing-info', function(e){
        e.preventDefault();
        $(this).next().addClass("hide");
        $(this).next().next().removeClass("hide");
    });

    $(document).on('click', '.save-billing-info', function(e){
        e.preventDefault();
        var val = $(this).prev().val();

        if (val != null && val != "") {
            $(this).parent().addClass("hide");
            $(this).parent().prev('p').html(val).removeClass("hide");
        } else {
            notifyMe.open({ 
                type: "error", 
                content: "Lütfen fatura adresinizi giriniz.", 
                delay: 3000
            });
        }
    });
    $(document).on('click', '#user_agreement', function(e){

        var $div = $('.shopping-cart-box'),
            dest= $div.offset().top - 10;

        $div.addClass("active");
        $('html,body').animate({scrollTop:dest}, 500,'swing');
    });
    
    $(document).on('click', '.choose-invester-btn', function(e){

        var $div = $('.sn-active-investers'),
            $parent = $(this).parent('.choose-invester-box'),
            investerID = $parent.data('invester-id'),
            name = $parent.find('.name').html(),
            surname = $parent.find('.surname').html(),
            fullname = name + " " + surname,
            dest= $div.offset().top - 10,
            curr = $('.active-invester').html(),
            currInvesterNum = parseInt(curr),
            one = 1,
            investerNum = currInvesterNum + one;

        if ($div.hasClass('active')) {  
            
            $(this).html('Seçili').addClass('disabled').prop('disabled', 'disabled');
            $('#invester_tags').tagsinput('add', { id: investerID, text: fullname });
            $div.find('.active-invester').html(investerNum);

        } else {            
            $(this).html('Seçili').addClass('disabled').prop('disabled', 'disabled');
            $div.addClass('active');
            $('#invester_tags').tagsinput('add', { id: investerID, text: fullname });
            $('html,body').animate({scrollTop:dest}, 500,'swing');
            $div.find('.active-invester').html(investerNum);
        
        }
    });

    $(document).on('click', '.choose-comparison-btn', function(e){

        var $this = $(this),
            $div = $('.report-comparison'),
            projectID = $this.data('project-id'),
            projectName = $this.data('project-name'),
            dest= $div.offset().top - 10,
            curr = $('.active-projects').html(),
            currProjectsNum = parseInt(curr),
            one = 1,
            projectsNum = currProjectsNum + one;

        if ($div.hasClass('active')) {  
            
            $this.html('Seçili').addClass('disabled').prop('disabled', 'disabled');
            $('#secili_projeler').tagsinput('add', { id: projectID, text: projectName });
            $div.find('.active-projects').html(projectsNum);
            $('html,body').animate({scrollTop:dest}, 500,'swing');
            $('.project-comparison-btn').addClass('aktif');

        } else {            
            $this.html('Seçili').addClass('disabled').prop('disabled', 'disabled');
            $div.addClass('active');
            $('#secili_projeler').tagsinput('add', { id: projectID, text: projectName });
            $('html,body').animate({scrollTop:dest}, 500,'swing');
            $div.find('.active-projects').html(projectsNum);
            $('.project-comparison-btn').addClass('aktif');
        }
    });

    $(document).on('click', '.share-photo', function(e){
        e.preventDefault();

        if (!isShareAttached) {
            $('.uploadPhoto').click();
        } else {
            notifyMe.open({ 
                type: "error", 
                content: "Yalnızca fotoğraf, video ya da yer ekleyebilirsiniz.", 
                delay: 3000
            });
        }
       
    });

    $(document).on('change', '.uploadPhoto', function(){
        readIMG(this);
        $(this).val();
        isShareAttached = true;
    });

    $(document).on('click', '.attach-close', function(){
        var $el = $(this).closest(".attach-box");

        if ($el.hasClass("image")) {
            $(this).parent().attr("src", "");
            $('.attach-box.image').removeClass('opened');

            if (!$(".attach-box").hasClass("opened")) {
                $('.share-attach').removeClass('active');    
            }
        } else if ($el.hasClass("video")) {
            $('.attach-box.video').removeClass('opened');
            
            if (!$(".attach-box").hasClass("opened")) {
                $('.share-attach').removeClass('active');    
            }
        }  else if ($el.hasClass("map")) {
            $('.attach-box.map').removeClass('opened');
            
            if (!$(".attach-box").hasClass("opened")) {
                $('.share-attach').removeClass('active');    
            }
        }
        
        isShareAttached = false;
    });

    $(document).on('click', '.share-video', function(){

        if (!isShareAttached) {
            $('.uploadVideo').click();
        } else {
            notifyMe.open({ 
                type: "error", 
                content: "Yalnızca fotoğraf ya da video ekleyebilirsiniz.", 
                delay: 3000
            });
        }
    });

    $(document).on('change', '.uploadVideo', function(){
        $(this).val();
        isShareAttached = true;
        $('.share-attach').addClass('active');
        $('.attach-box.video').addClass('opened');
    });

    $(document).on('click', '.share-place', function(){

       $('.attach-box.map').addClass('opened');

    });

    $(document).on('click', '.share-send', function(){
        var shareText = $('.share-textarea').val(),
            sharePic = $('.uploadPhoto').val(),
            shareVideo = $('.uploadVideo').val();
    });

    $('.msg-to-input').keyup(function(e){
    	if(e.keyCode == 13) {
    		var $this = $(this);
    		sendMsg($this,true);
    	}
    });
    
    $(document).on('closed', '.remodal', function (e) {
        if ($("#sn_video_edit").length) {
            var $vid_obj = videojs("#sn_video_edit");
            $vid_obj.pause();
        }        
    });

    $('.mentor-message').keyup(function(e){
        if(e.keyCode == 13) {
            var $this = $(this);
            mentorChatSend($this,true);
        }
    });

    if ($(".profil_duzenle").length) {
        $(".profil_duzenle").validate( {
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Formunuz başarıyla gönderildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                profile_name: {
                    required: true,
                    minlength: 3
                },
                profile_desc: {
                    required: true,
                    maxlength: 80
                },
                profile_picture: {
                    required: true,
                    accept: "image/*"
                },
                profile_about: {
                    required: true,
                    maxlength: 1000
                },
                profile_country: { valueNotEquals: "0" },
                profile_city: { valueNotEquals: "0" },
                profile_type: { valueNotEquals: "0" },
                profile_phone: { 
                    required: true,
                    number: true
                }
            },
            messages: {
                profile_name: { 
                    required: "Ad soyad boş bırakılamaz",
                    minlength: "En az 3 karakter girmelisiniz"
                },
                profile_desc: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 80 karakter girebilirsiniz"
                },
                profile_picture: {
                    required: "Lütfen bir fotoğraf seçiniz",
                    accept: "Sadece resim (jpg/png/gif) formatında yükleme yapınız"
                },
                profile_about: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 1000 karakter girebilirsiniz"
                },
                profile_country: { valueNotEquals: "Bu alan boş bırakılamaz" },
                profile_city: { valueNotEquals: "Bu alan boş bırakılamaz" },
                profile_type: { valueNotEquals: "Bu alan boş bırakılamaz" },
                profile_phone: "Lütfen geçerli bir telefon numarası giriniz"
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if ( element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                }  else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }
    if ($(".proje_yarat").length) {
        $(".proje_yarat").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Formunuz başarıyla gönderildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                project_name: {
                    required: true,
                    minlength: 3
                },
                evelator_pitch: {
                    required: true,
                    maxlength: 80
                },
                project_about: {
                    required: true,
                    maxlength: 1000
                },
                project_logo_file: {
                    required: true,
                    accept: "image/*"
                },
                project_url: "required",
                project_category: { valueNotEquals: "0" },
                project_country: { valueNotEquals: "0" },
                project_city: { valueNotEquals: "0" },
                project_status: { valueNotEquals: "0" },
                project_investement_status: { valueNotEquals: "0" }
            },
            messages: {
                project_name: { 
                    required: "Ad soyad boş bırakılamaz",
                    minlength: "En az 3 karakter girmelisiniz"
                },
                evelator_pitch: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 80 karakter girebilirsiniz"
                },
                project_about: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 1000 karakter girebilirsiniz"
                },
                project_logo_file: {
                    required: "Lütfen bir fotoğraf seçiniz",
                    accept: "Sadece resim (jpg/png/gif) formatında yükleme yapınız"
                },
                project_url: "Bu alan boş bırakılamaz",
                project_category: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_country: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_city: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_status: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_investement_status: { valueNotEquals: "Bu alan boş bırakılamaz" }
                
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".proje_duzenle").length) {
        $(".proje_duzenle").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Formunuz başarıyla gönderildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                project_name: {
                    required: true,
                    minlength: 3
                },
                evelator_pitch: {
                    required: true,
                    maxlength: 80
                },
                project_about: {
                    required: true,
                    maxlength: 1000
                },
                project_logo_file: {
                    required: true,
                    accept: "image/*"
                },
                project_url: "required",
                project_category: { valueNotEquals: "0" },
                project_country: { valueNotEquals: "0" },
                project_city: { valueNotEquals: "0" },
                project_status: { valueNotEquals: "0" },
                project_investement_status: { valueNotEquals: "0" }
            },
            messages: {
                project_name: { 
                    required: "Ad soyad boş bırakılamaz",
                    minlength: "En az 3 karakter girmelisiniz"
                },
                evelator_pitch: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 80 karakter girebilirsiniz"
                },
                project_about: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 1000 karakter girebilirsiniz"
                },
                project_logo_file: {
                    required: "Lütfen bir fotoğraf seçiniz",
                    accept: "Sadece resim (jpg/png/gif) formatında yükleme yapınız"
                },
                project_url: "Bu alan boş bırakılamaz",
                project_category: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_country: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_city: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_status: { valueNotEquals: "Bu alan boş bırakılamaz" },
                project_investement_status: { valueNotEquals: "Bu alan boş bırakılamaz" }
                
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".hesap_ayarlari").length) {
        $(".hesap_ayarlari").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Hesap ayarlarınız başarıyla değiştirildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                profile_password: "required",
                profile_password_again: {
                  equalTo: "#profile_password"
                }
            },
            messages: {
                profile_password: "Bu alan boş bırakılamaz",
                profile_password_again: "Parolalar aynı olmak zorundadır"               
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".email_ayarlari").length) {
        $(".email_ayarlari").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Email ayarlarınız başarıyla değiştirildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                profile_email: {
                  required: true,
                  email: true
                }

            },
            messages: {
                profile_email: {
                    required: "Bu alan boş bırakılamaz",
                    email: "Lütfen geçerli bir email giriniz"
                }
                             
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".create-community").length) {
        $(".create-community").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Formunuz başarıyla gönderildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                community_name: {
                    required: true,
                    minlength: 3
                },
                community_purpose: {
                    required: true,
                    maxlength: 80
                },
                community_about: {
                    required: true,
                    maxlength: 1000
                },
                community_logo: {
                    required: true,
                    accept: "image/*"
                },
                community_url: "required",
                community_country: { valueNotEquals: "0" },
                community_city: { valueNotEquals: "0" },
            },
            messages: {
                community_name: { 
                    required: "Bu alan boş bırakılamaz",
                    minlength: "En az 3 karakter girmelisiniz"
                },
                community_purpose: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 80 karakter girebilirsiniz"
                },
                community_about: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 1000 karakter girebilirsiniz"
                },
                community_logo: {
                    required: "Lütfen bir fotoğraf seçiniz",
                    accept: "Sadece resim (jpg/png/gif) formatında yükleme yapınız"
                },
                community_url: "Bu alan boş bırakılamaz",
                community_country: { valueNotEquals: "Bu alan boş bırakılamaz" },
                community_city: { valueNotEquals: "Bu alan boş bırakılamaz" },
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".edit-community").length) {
        $(".edit-community").validate({
            submitHandler: function(form) {
                notifyMe.open({ 
                    type: "success", 
                    content: "Formunuz başarıyla gönderildi", 
                    delay: 3000
                });
                form.submit();
            },
            rules: {
                community_name_edit: {
                    required: true,
                    minlength: 3
                },
                community_purpose_edit: {
                    required: true,
                    maxlength: 80
                },
                community_about_edit: {
                    required: true,
                    maxlength: 1000
                },
                community_logo_edit: {
                    required: true,
                    accept: "image/*"
                },
                community_url_edit: "required",
                community_country_edit: { valueNotEquals: "0" },
                community_city_edit: { valueNotEquals: "0" },
            },
            messages: {
                community_name_edit: { 
                    required: "Bu alan boş bırakılamaz",
                    minlength: "En az 3 karakter girmelisiniz"
                },
                community_purpose_edit: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 80 karakter girebilirsiniz"
                },
                community_about_edit: {
                    required: "Bu alan boş bırakılamaz",
                    maxlength: "En fazla 1000 karakter girebilirsiniz"
                },
                community_logo_edit: {
                    required: "Lütfen bir fotoğraf seçiniz",
                    accept: "Sadece resim (jpg/png/gif) formatında yükleme yapınız"
                },
                community_url_edit: "Bu alan boş bırakılamaz",
                community_country_edit: { valueNotEquals: "Bu alan boş bırakılamaz" },
                community_city_edit: { valueNotEquals: "Bu alan boş bırakılamaz" },
            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "file") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if ($(".payment_form").length) {
        $(".payment_form").validate({
            submitHandler: function(form) {

                form.submit();

            },
            rules: {
                payment_method: {
                    required: true
                },
                card_owner: {
                    required: true
                },
                card_number: {
                    required: true
                },
                card_security_code: {
                    required: true
                },
                card_exp_month: {
                    required: true
                },
                card_exp_year: {
                    required: true
                },
                user_agreement: {
                    required: true
                }
            },
            messages: {
                payment_method: "Bu alan boş bırakılamaz",
                card_owner: "Bu alan boş bırakılamaz",
                card_number: "Bu alan boş bırakılamaz",
                card_security_code: "Bu alan boş bırakılamaz",
                card_exp_month: "Bu alan boş bırakılamaz",
                card_exp_year: "Bu alan boş bırakılamaz",
                user_agreement: "Bu alan boş bırakılamaz",

            },
            errorElement: "em",
            errorPlacement: function (error, element) {
                error.addClass("help-block");

                if (element.attr("type") === "checkbox") {
                    error.insertAfter(element.parent("label"));
                } else if (element.attr("type") === "radio") {
                    error.insertAfter(element.parent().next());
                } else {
                    error.insertAfter(element);
                }
            },
            highlight: function (element, errorClass, validClass) {
                $( element ).parents(".form-col").addClass("has-error").removeClass("has-success");
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents(".form-col").removeClass("has-error");
            }
        });
    }

    if (windowWidth == "1024" || windowWidth == "768") {
    	$(".user-icon.messages").attr("data-horizontal-offset","10");
    	$("#user-dropdown-messages").addClass("jq-dropdown-anchor-right");
    	$(".user-icon.notifications").attr("data-horizontal-offset","10");
    	$("#user-dropdown-notifications").addClass("jq-dropdown-anchor-right");
    	$(".user-dd").attr("data-horizontal-offset","10");
    	$("#user-dropdown-settings").addClass("jq-dropdown-anchor-right");
    }
    if (windowWidth == "425") {
    	$(".user-icon.messages").attr("data-horizontal-offset","50");
    	$("#user-dropdown-messages").addClass("jq-dropdown-anchor-50");
    	$(".user-icon.notifications").attr("data-horizontal-offset","50");
    	$("#user-dropdown-notifications").addClass("jq-dropdown-anchor-50");
    	$(".user-dd").attr("data-horizontal-offset","0");
    	$("#user-dropdown-settings").addClass("jq-dropdown-anchor-right");
        $(".store-choose").attr("data-horizontal-offset","0");
        $("#store-choose-project").addClass("jq-dropdown-anchor-right");
    }
    if (windowWidth == "375") {
    	$(".user-icon.messages").attr("data-horizontal-offset","100");
    	$("#user-dropdown-messages").addClass("jq-dropdown-anchor-100");
    	$(".user-icon.notifications").attr("data-horizontal-offset","60");
    	$("#user-dropdown-notifications").addClass("jq-dropdown-anchor-60");
    	$(".user-dd").attr("data-horizontal-offset","0");
    	$("#user-dropdown-settings").addClass("jq-dropdown-anchor-right");
        $(".store-choose").attr("data-horizontal-offset","0%");
        $("#store-choose-project").addClass("jq-dropdown-anchor-left");
    }
    if (windowWidth == "320") {
    	$(".user-icon.messages").attr("data-horizontal-offset","110");
    	$("#user-dropdown-messages").addClass("jq-dropdown-anchor-110");
    	$(".user-icon.notifications").attr("data-horizontal-offset","60");
    	$("#user-dropdown-notifications").addClass("jq-dropdown-anchor-60");
    	$(".user-dd").attr("data-horizontal-offset","0");
    	$("#user-dropdown-settings").addClass("jq-dropdown-anchor-right");
    	$(".user-icon.friend-requests").attr("data-horizontal-offset","-130");
        $(".how-many.likes").attr("data-horizontal-offset","40");
        $(".how-many.likes").attr("data-vertical-offset","20");
        $("#feed-likes-dropdown").addClass("jq-dropdown-anchor-60");
        $(".store-choose").attr("data-horizontal-offset","-100%");
        $("#store-choose-project").addClass("jq-dropdown-anchor-140");
    }


})();
function addCommas(t) {
      return String(t).replace(/(\d)(?=(\d{3})+$)/g, "$1.")
}

function tableUpdate(sale,expense) {
    var currSalePrice = $('.sale-sum').html(),
        currExpPrice = $('.expense-sum').html(),
        currSaleSplit = addCommas(sale),
        currExpSplit = addCommas(expense),
        floatSale = parseFloat(currSaleSplit);
        floatExp = parseFloat(currExpSplit);

        

        if (isNaN(currSalePrice)) {
            var currSaleP = parseFloat(currSalePrice);
            
            currSale = +currSaleP + +floatSale; 

        } else {
            currSale = floatSale;
        }
        
        if (isNaN(currExpPrice)) {
            var currExpP = parseFloat(currExpPrice);
                

            currExp = +currExpP + +currExpSplit;  
        } else {
            currExp = floatExp;
        }
        
        var fixedSale = currSale.toFixed(3),
            fixedExp = currExp.toFixed(3),
            sumSale = fixedSale +".00";
            sumExp = fixedExp + ".00";

        $('.sale-sum').html(sumSale);
        $('.expense-sum').html(sumExp);
}

function readIMG(input) {
    var $image = $('.share-image');

    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $image.attr("src", e.target.result);
            $('.share-attach').addClass('active');
            $('.attach-box.image').addClass('opened');
        }
            
        reader.readAsDataURL(input.files[0]);
    }
}

function readMsg(el) {
	var $this = $(el);

	var msgID = $this.data('message-id');
	$('.user-messages').find('.message-preview').hide();
	$('div').find("[data-msg-id='" + msgID + "']").show();
	$(".message-row").mCustomScrollbar("scrollTo","bottom");
}

function userGoBack(from) {
	if (from == "message-wrap") {
		$('.message-wrap').hide();
		$('.user-messages').find('.message-preview').show();
	} else if (from == "choose-user") {
		$('.choose-user').hide();
		$('.user-messages').find('.message-preview').show();	
	}  else if (from == "new-message") {
		$('.new-message').hide();
		$('.user-messages').find('.message-preview').show();	
	} 
}
	
function sendMsg(el,keyup) {
	var $this = $(el);
    var d = new Date(),
        h = (d.getHours()<10?'0':'') + d.getHours(),
        m = (d.getMinutes()<10?'0':'') + d.getMinutes(),
        timeNow = h + ':' + m;

	if (keyup) {
		var message = $this.val();	
	} else {
		var message = $this.prev(".msg-to-input").val();
	}
    
    if (message == null || message == "") {
		return false;
	} else {
		var messageDiv = $("<div>", {class: "msg-content sent"});
		var messageContent =  $("<p></p>");
		$this.parent().prev(".message-row").find('.mCSB_container').prepend(messageDiv);

		messageDiv.append(messageContent);
		messageContent.html(message);
		messageDiv.append("<span>Bugün " + timeNow + "</span>");
		$(".message-row").mCustomScrollbar("scrollTo","bottom");
		$(".msg-to-input").val("");
	}
}

function newMsg(el,userID) {
	var $this = $(el);

	$(".choose-user").hide();
	$(".new-message").show();
}

function mentorChatSend(el,keyup) {

    var $this = $(el);
    var d = new Date(),
        h = (d.getHours()<10?'0':'') + d.getHours(),
        m = (d.getMinutes()<10?'0':'') + d.getMinutes(),
        timeNow = h + ':' + m;

    if (keyup) {
        var message = $this.val();  
    } else {
        var message = $this.prev(".mentor-message").val();
    }
    
    if (message == null || message == "") {
        return false;
    } else {
        var messageDiv = $("<div>", {class: "msg-content sent"});
        var messageContent =  $("<p></p>");
        $this.parent().prev(".mentor-chat-box").find('.mCSB_container').prepend(messageDiv);

        messageDiv.append(messageContent);
        messageContent.html(message);
        messageDiv.append("<span>Bugün " + timeNow + "</span>");
        $(".mentor-chat-box").mCustomScrollbar("scrollTo","bottom");
        $(".mentor-message").val("");
    }
}

function smokeTestAnswer(n,a,el){
    var $this = $(el),
        $input = $this.closest('.answer-form').find('.answer-input'),
        $buttons = $this.closest('.answer-form').find('.btn-default'),
        $formInputs = $('smoke-test-answer-form').find("input");

    $buttons.each(function() {
        if ($(this).hasClass('btn-reverse')) {
            $(this).removeClass('btn-reverse');
        }    
    });
    
    $this.addClass('btn-reverse');
    $input.val(a);

    var full = 0;

    $('.answer-input').each(function () {
        if ($.trim(this.value) == "") full++;
    });
    if (!full > 0) {
        $('.answer-submit').removeClass('btn-disabled').prop("disabled", "");
    }
}