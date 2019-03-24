// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.


"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();
var room = document.getElementById("questionId").innerHTML;

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", addMessage);
connection.on("ReceiveMessages", addMessages);
connection.on("AlertCorrectIsFound", addcorrectmessages);
function addcorrectmessages(user,message) {
    var nameSpan = document.createElement('span');
    nameSpan.className = 'user';
    nameSpan.textContent = user;
    var headerDiv = document.createElement('div');
    headerDiv.appendChild(nameSpan);

    var messageDiv = document.createElement('div');
    messageDiv.className = 'message';
    messageDiv.textContent = message;


    var newItem = document.createElement('li');

    newItem.SET
    newItem.appendChild(headerDiv);
    newItem.appendChild(messageDiv);
    
    var divitem = document.createElement('div');

    divitem.append(newItem);

    document.getElementById("correctMessagesList").appendChild(divitem);

}


connection.start().then(function () {
    
    connection.invoke("JoinRoom", room)
    connection.invoke("LoadHistory", room)
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    event.preventDefault();
    //var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", room, message).catch(function (err) {
        return console.error(err.toString());
    });
    document.getElementById("messageInput").value = '';
    document.getElementById("sendButton").disabled = true;


    
});
document.getElementById("messageInput").addEventListener("input", function (event) {
    if (document.getElementById("messageInput").value.length != 0) {
        document.getElementById("sendButton").disabled = false;
    }

})

document.getElementById("messageInput").oninput = function () {
    if (document.getElementById("messageInput").value.length === 0) {
        document.getElementById("sendButton").disabled = true;
    }
}

function addMessages(messages) {
    if (!messages) return;

    messages.forEach(function (m) {
        addMessage(m.user, m.time, m.message);
    });
}

function addMessage(user, time, message) {
    var nameSpan = document.createElement('span');
    nameSpan.className = 'user';
    nameSpan.textContent = user;

    var timeSpan = document.createElement('span');
    timeSpan.className = 'time';
    var friendlyTime = moment(time).format('H:mm');
    timeSpan.textContent = friendlyTime;

    var headerDiv = document.createElement('div');
    headerDiv.appendChild(nameSpan);
    headerDiv.appendChild(timeSpan);

    var messageDiv = document.createElement('div');
    messageDiv.className = 'message';
    messageDiv.textContent = message;

    var checkbox = document.createElement('input');
    checkbox.type = 'checkbox';
    checkbox.name = 'check';
    checkbox.id = "check";
    checkbox.value = "Mark as Right Answer";
    
    //checkbox.addEventListener('change', function () {
    //    var allCheckBoxes = document.getElementsByName('check');
       
    //    for (var i = 0; i < allCheckBoxes.length; i++) {
    //        allCheckBoxes[i].checked = false;
    //    }

    //    checkbox.checked = true;
        
        
    //});
    var rightButton = document.createElement('input');
    rightButton.type = 'button';
    rightButton.value = "Mark as right Answer";
    rightButton.className = 'rightAnswerBtn';
    rightButton.id = 'rightAnswerBtn';
    rightButton.addEventListener('click', function () {
        var allCheckBoxes = document.getElementsByName('check');

        for (var i = 0; i < allCheckBoxes.length; i++) {
            allCheckBoxes[i].checked = false;
        }
        checkbox.checked = true;

       // document.getElementById("messagesList").insertBefore(divitem, document.getElementById("messagesList").firstChild);
        if (confirm('Are you sure this is the right answer??')) {

            connection.invoke("CorrectAnswer", room, user, message)
           
        } else {
            // Do nothing!
        }
        //connection.invoke("CorrectAnswer", room, messageDiv.textContent);
    })

   



    var newItem = document.createElement('li');
    
    newItem.SET
    newItem.appendChild(headerDiv);
    newItem.appendChild(messageDiv);
    newItem.appendChild(checkbox);
    newItem.appendChild(rightButton);

    var divitem = document.createElement('div');

    divitem.append(newItem);

    document.getElementById("messagesList").appendChild(divitem);
   

}







