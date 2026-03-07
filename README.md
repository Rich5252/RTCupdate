PC clock updater for use with wsjt-x to help tune clock timing especially with FT2 that requires tight clock control. Reads NTP time every 15 minutes (or on demand) and sets the PC clock with the entered offset (+ve number is a delay - same as JTsync app).

This app does a similar job to JTsync but funtions are more aligned for use with FT2 that requires more dynamic clock settings for maximum decode success.

FT2 decode requires that the transmission is received within 500ms from the beginning of the period. WSJTX of course uses the local PC clock time to determine the beginning of the period so this can be manipulated to compensate for offsets (errors and delays) at the sender's PC.

If you watch "DT" reported by WSJTX you'll see on FT2 that the max DT is 500ms. Anyone sending later than this will not be decoded. However if you delay your clock then it will bring these "late" transmissions into the local window.

Note that FT4 has a 1 second sync window so is much less critical. FT8 uses a different decode methodology and the transmission just needs to end before the end of period.

Remember to consider your delays in transmission. My TX is part of my homebrew SDRUno based system and uses UDP datagrams to control the TX. I see delays from about 50 to 150ms on UDP TX commands. Also consider the SDR latency time. This effectively adds delays to the received signal (making it appear late) and on my SDRUno sytem I've measured this as 125 - 175s so quite significant in FT2 terms. So the UDP and SDR latency typically add ~ 200ms delay and this eats into the 500ms available when the receiver is exactly synced to UTC.

On FT2 look out for signals in waterfall that are looking late in period and not decodeing. Increase the delay using the Inc button and bring the signal into decode window. Typically the minimum delay to achieve decode will give you best chance of completeing a QSO. For normal operation I set 150 or 200ms as default and find this is optimal in my SDR system case.
