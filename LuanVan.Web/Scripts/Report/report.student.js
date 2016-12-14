$(function () {
    $.ajax({
        type: 'GET',
        url: '/report/getstudentreport',
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                var studentId = 'student-report-' + result[i].studentId;
                $('#student-report').append(
                    "<h2>" + result[i].studentName + "</h2>" +
                    "<div class=\"" + studentId + " ct-major-eleventh\"></div><br/>");

                new Chartist.Line('.' + studentId,
                    {
                        labels: result[i].dates,
                        series: [result[i].practicedWords]
                    },
                    {
                        axisY: {
                            labelInterpolationFnc: function (value) {
                                return (value % 1 === 0) ? value : '';
                            }
                        }
                    }
                );
            }

            
        }
    });
});