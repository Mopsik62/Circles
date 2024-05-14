mergeInto(LibraryManager.library, {


  RateGame: function () {
    ysdk.feedback.canReview()

        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                        console.log("You rate the game");
                    })
            } else {
                console.log(reason)
                console.log("You can't rate the game");

            }
        })

  },

  Hello: function (value) {
    console.log("Hello function = " + value);
  },

  SaveExtern: function (date) {
    var dateString = UTF8ToString(date);
    var myobj = JSON.parse(dateString);
    player.setData(myobj);
    console.log("myobj");
    console.log(myobj);
    console.log("PlayerData was saved");

  },


  LoadExtern: function () {
    player.getData().then(_date =>{
      const myJSON = JSON.stringify(_date);
      console.log("myJSON");

      console.log(myJSON);
      MyGameInstance.SendMessage('GameManager', 'LoadFromYandex', myJSON);
      console.log("PlayerData was loaded");
    });
  },

  SetToLeadeboard: function(value){
    ysdk.getLeaderboards()
    .then(lb => {
      lb.SetToLeaderboardScore('HighScore', value)
    });
  },



  
}); 