document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridWeek',
        height: 600,
        events: '/Schedule/GetEvents', // Fetch events from the server
        weekends: true,
        eventContent: function (info) {
            var enrolButton = document.createElement('button');
            enrolButton.innerHTML = 'Enrol';
            enrolButton.className = 'enrol-btn';

            // Add a listener to the enroll button
            enrolButton.addEventListener('click', function () {
                console.log(info.event.extendedProps.coachName); // Log coachName to verify it's correct
                enrolEvent(info.event.title, info.event.start, info.event.extendedProps.coachName);
            });

            var eventTitle = document.createElement('div');
            eventTitle.innerHTML = info.event.title;

            var arrayOfDomNodes = [eventTitle, enrolButton];
            return { domNodes: arrayOfDomNodes };
        }
    });

    calendar.render();
});
