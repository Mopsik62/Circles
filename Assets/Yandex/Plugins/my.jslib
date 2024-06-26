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

    SetToLeaderboard: function(value){
      ysdk.getLeaderboards()
      .then(lb => {
        lb.SetLeaderboardScore('HighScore', value)
      });
    },


   GetLang: function() {
        // Получение языка из инициализированного SDK
        var lang = ysdk.environment.i18n.lang;
        console.log(lang);

        // Преобразование строки в буфер
        var bufferSize = lengthBytesUTF8(lang) + 1;
        var buffer = _malloc(bufferSize);
        stringToUTF8(lang, buffer, bufferSize);
        return buffer;
   },
   
    LoadExtern: async function() {
    try {
        // Ожидание инициализации игрока
        await initPlayer();

        if (player) {
            const _data = await player.getData();
            const myJSON = JSON.stringify(_data);
            console.log("myJSON");
            console.log(myJSON);
            MyGameInstance.SendMessage('GameManager', 'LoadFromYandex', myJSON);
            console.log("PlayerData was loaded");
        } else {
            console.error('Player not initialized');
        }
    } catch (err) {
        console.error('Error loading player data:', err);
    }
    },

   ShowAdv : function(){
      ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          console.log("===============closed===============================");
          // Действие после закрытия рекламы.
        },
        onError: function(error) {
          // Действие в случае ошибки.
        }
    }
})
   },




    
  }); 