$(function () {
    $.ajax({
        type: 'GET',
        url: '/report/getlessonlikereport',
        success: function (result) {
            var sum = function (a, b) { return a + b };

            for (var i = 0; i < result.length; i++) {
                var lessonId = 'lesson-like-report-' + result[i].lessonId;

                $('#lesson-like-report').append(
                    "<h2>Who likes " + result[i].lessonName + "</h2>" +
                    "<div class=\"" + lessonId + " ct-major-eleventh\"></div><br/>");

                var data = {
                    series: [result[i].dislike, result[i].noIdea, result[i].like]
                };

                //if (result[i].dislike != 0) {
                //    data.series.push(result[i].dislike);
                //}

                //if (result[i].noIdea != 0) {
                //    data.series.push(result[i].noIdea);
                //}

                //if (result[i].like != 0) {
                //    data.series.push(result[i].like);
                //}

                new Chartist.Pie('.' + lessonId, data, {
                    labelInterpolationFnc: function (value) {
                        return Math.round(value / data.series.reduce(sum) * 100) + '%';
                    }
                });

                //var data = {
                //    labels: [result[i].dislike, result[i].noIdea, result[i].like],
                //    series: [result[i].dislike, result[i].noIdea, result[i].like]
                //};

                //var options = {
                //    labelInterpolationFnc: function (value) {
                //        return value[0]
                //    }
                //};

                //var responsiveOptions = [
                //  ['screen and (min-width: 640px)', {
                //      chartPadding: 30,
                //      labelOffset: 100,
                //      labelDirection: 'explode',
                //      labelInterpolationFnc: function (value) {
                //          return value;
                //      }
                //  }],
                //  ['screen and (min-width: 1024px)', {
                //      labelOffset: 80,
                //      chartPadding: 20
                //  }]
                //];

                //new Chartist.Pie('.' + lessonId, data, options, responsiveOptions);
            }
        }
    });
});