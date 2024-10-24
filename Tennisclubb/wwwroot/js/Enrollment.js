function enrolEvent(eventName, eventDate, coachName) {
    $.ajax({
        type: 'POST',
        url: '/Enrollment/AddToHistory',
        data: {
            eventName: eventName,
            eventDate: eventDate,
            coachName: coachName
        },
        success: function (response) {
            if (response.success) {
                alert('Enrollment successful!');
                // Cập nhật lịch sử
                updateEnrollmentHistory(eventName, eventDate);
            } else {
                alert(response.message);
            }
        },
        error: function () {
            alert('Error: Could not enroll in the event.');
        }
    });
}

function updateEnrollmentHistory(eventName, eventDate) {
    var historyList = $('#enrollmentHistoryList');
    historyList.append(`<li>${eventName} - ${new Date(eventDate).toLocaleDateString()}</li>`);
}