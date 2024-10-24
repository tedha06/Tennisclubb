<script>
    document.addEventListener('DOMContentLoaded', function () {
        var calendarEl = document.getElementById('calendar');

    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'dayGridWeek',
    height: 600,
    events: '/Schedule/GetEvents', // Load events from server
    weekends: true,
    eventContent: function (info) {
                var enrolButton = document.createElement('button');
    enrolButton.innerHTML = 'Enrol';
    enrolButton.className = 'enrol-btn';

    enrolButton.addEventListener('click', function () {
        enrolEvent(info.event.id); // Use the event ID for enrollment
                });

    var eventTitle = document.createElement('div');
    eventTitle.innerHTML = info.event.title;

    var arrayOfDomNodes = [eventTitle, enrolButton];
    return {domNodes: arrayOfDomNodes };
            }
        });

    calendar.render();
    });

    function enrolEvent(eventId) {
        console.log('Enrolling in event:', eventId); // Log the event ID

    $.ajax({
        type: 'POST',
    url: '/Schedule/Enroll',  // Updated URL
    data: {
        scheduleId: eventId // Send event ID to backend
            },
    success: function (response) {
                if (response.success) {
        alert('Enrollment successful for event: ' + response.eventName);
    window.location.href = '/Enrollment/History'; // Redirect to history page
                } else {
        alert(response.message || 'Error: Could not enroll in the event.');
                }
            },
    error: function () {
        alert('Error occurred while enrolling in the event.');
            }
        });
    }
</script>