$(function () {
    $.ajax({
        type: 'GET',
        url: '/report/getlessonreport',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var lessonId = 'lesson-report-' + result[i].lessonId;
                $('#lesson-report').append(
                    "<h2>" + result[i].lessonName + "</h2>" +
                    "<div class=\"" + lessonId + " ct-major-eleventh\"></div><br/>");

                new Chartist.Bar('.' + lessonId,
                    {
                        labels: result[i].students,
                        //labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri'],
                        series: [result[i].wrongTimes]
                        //series: [5, 2, 4, 2, 0]
                    },
                    {
                        reverseData: true,
                        horizontalBars: true,
                        axisX: {
                            labelInterpolationFnc: function (value) {
                                return (value % 1 === 0) ? value : '';
                            }
                        },
                        axisY: { offset: 70 },
                    }
                );
            }
        }
    });
});